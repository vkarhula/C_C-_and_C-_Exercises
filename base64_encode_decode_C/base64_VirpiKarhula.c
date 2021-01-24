/****************************************************************
 * 
 * Tiedosto: base64.c
 *
 * Ohjelman toiminta:
 * Luetaan sy�tett� riveitt�in (fgets(), dekoodaus) 
 * tai LINESIZE-m��r� merkkej�  kerrallaan (fread(), enkoodaus).
 * Jos ensimm�inen rivi on kokonaan base64-koodeja, siirryt��n
 * koko tiedoston dekoodaukseen (tila 1). Jos seasta l�ytyy 
 * non-base64-koodi, annetaan virheilmoitus ja keskeytet��n koodaus.
 *
 * Jos kyseess� on MIME-viesti, etsit��n 'Content-type:"-teksti�.
 * Jos 'Content-type:'-teksti l�ytyy, etsit��n 'multipart'-teksti�.
 * 'multipart'-tekstin perusteella tehd��n jako tiloihin 2 tai 3.
 * Tilan 2 (ei multipart) dekoodaus alkaa tyhj�n rivin j�lkeen.
 * Tilan 3 (multipart) dekoodaus alkaa, jos erotinteksti ja 
 * 'Content-Type-Encoding: base64'-teksti on l�ydetty eik� niit� erota 
 * tyhj� rivi. Dekoodaus alkaa tyhj�n rivin j�lkeen ja 
 * katkeaa tyhj��n riviin, jonka j�lkeen palataan etsim��n erotinteksti� 
 * ja 'C-T-E: base64'-teksti�. 
 * Enkoodaus-tilan valinta (tila 4) tehd��n komentoriviparametrin 
 * perusteella.
 * Ellei mik��n dekoodausehto t�yty, sy�tteelle ei tehd� mit��n.
 * Kaikki sy�tteet enkoodataaan.
 *
 * k��nn�s: cc -o base64 base64.c 
 *
 * Author Virpi Karhula
 * Date 19.12.2005
 *
 *********************************************************************/

#include <stdio.h>
#include <string.h>
#include <stdlib.h>
#include <ctype.h>

#define LINESIZE 100

size_t lueRivi(char ** argv, char * koodit, unsigned char *input, int * b, int * y);
int annaKoko(char * taulu);
size_t annaInputKoko(unsigned char * taulu);
int onkoTyhjaRivi(unsigned char *input);
int loytyykoBoundary(unsigned char *input, char *BOU);
int loytyykoBase64(unsigned char *input);
int paikannaBoundary(unsigned char * input, char * BOU);
int loytyykoContentType(unsigned char * input);
int loytyykoContentMulti(unsigned char *input, int ib);
int onkoRiviBase64(unsigned char * input, char * koodit, size_t inputLkm);
void kutsuEncode64(char * koodit, unsigned char * input, int * e, int * b, int *y, int * rivi, size_t inputLkm);
void encode64(char *koodit, int * e, int * b, int y);
void kutsuDecode64(char *koodit, unsigned char * input, char * d, size_t inputLkm);
void decode64(char * koodit, char * d, int yht);
//void tulostaCharTaulu(char *t, int koko);


int main(int argc, char * argv[]){
  // Input stream
  unsigned char input[LINESIZE];  // Sy�te luetaan input-taulukkoon
  int i = 0;
  for( i = 0; i < (LINESIZE -1); i++){
    input[i] = 0;    // Alustus
  }
  input[LINESIZE - 1] = ' \0';
  size_t inputLkm = 0;  // input-muuttujan koko

  char koodit[64] = \
    {'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', \
     'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', \
     'U', 'V', 'W', 'X', 'Y', 'Z', 'a', 'b', 'c', 'd', \
     'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', \
     'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', \
     'y', 'z', '0', '1', '2', '3', '4', '5', '6', '7', \
     '8', '9', '+', '/'};

  // Enkoodaus
  int e[4] = {0x0, 0x0, 0x0, 0x0};  //encode, enkoodattavat merkit
  int b[4] = {0x0, 0x0, 0x0, 0x0};  //enkoodatut base64-koodit (encode64)
  int y = 3;  // laskuri, puskurissa olevien ja/tai luettujen 
              // tavujen lkm (kutsuEncode64)
              // y = 3 normaaliarvo (kolmen tavun ryp�s ei ole vajaa), 
              // 1 = yksi puskurissa, 2 = kaksi puskurissa
              // puskurina p��ohjelman muuttujat e[]
              // vied��n funktiokutsuissa v�lill� osoitteena, v�lill� kopiona
  int rivi = 0;  //rivinvaihto, kutsuEncode()
  // Dekoodaus
  char d[4] = {'(', '(', '(', '('};  // non-base64 -koodeja

  int tila = 0;     // ohjelman toiminnan jakautuminen eri tiloihin
  int ekaRivi = 0;  // tutkitaan onko sy�te base64-koodia vain 1.rivill�
  char BOU[100];    // boundary-teksti
  int boundaryLoytynyt = 0;  //false
  int b64 = 0;      // base64 ko. rivill�, multipart
  int b64non = 0;   // base64 ko.rivill�, non-multipart
  int multi = 0;    // multipart = 1
  int content = -1;  // palauttaa offsetin 'Content-type:'-tekstiin
  int bo = 0;       // erotusteksti ko. rivill�
  int etsiBo = 0;   // sallii boundaryn etsinn�n
  int etsiB64 = 0;  // false, CTE:base64 -etsint� estetty
  int de = 0;       // 1 = dekoodausalue
  int rr = 1;       // rivilaskuri (debuggaus)


  // Oikeiden aloitusparametrien tarkistus      
  if(argc >= 2){
    char * merkki[3];
    *merkki = "-e";    
    *(merkki+1) = "-d";
    // Valitsin -e tai -d hyv�ksyt��n vain toisena sis��nsy�tettyn� merkkin�
    if( strcmp(*(argv+1), *(merkki)) == 0){  //-e
      //printf("\n-e valittu");
      tila = 4;  //TILA, enkoodataan koko tiedosto
      //printf("\nTILA4");
    } else if( strcmp(*(argv+1), *(merkki+1)) == 0){  //-d
      //printf("\n-d valittu");
    } else {
      printf("\nK�ynnist� ohjelma seuraavasti:\n\n");
      printf("ohjelma -optio (< tai >) (tiedosto)");
      printf("\n\nmiss� optio: e enkoodaus ja d dekoodaus\n\n");
    }
  } else {
    printf("\nK�ynnist� ohjelma seuraavasti:\n\n");
    printf("ohjelma -optio (< tai >) (tiedosto)");
    printf("\n\nmiss� optio: e enkoodaus ja d dekoodaus\n\n");
    return(0);
  }

  // Luetaan stdin-sy�tett� rivi (dekoodaus) 
  // tai LINESIZE-merkki� kerrallaan (enkoodaus)
  // sy�tteen loppuun saakka

  do{

    inputLkm = lueRivi(argv, koodit, input, b, &y);

    // *** Tilasiirtym�t, tila 1 ***
    if (ekaRivi++ == 0){
      if (onkoRiviBase64(input, koodit, inputLkm) == 1){
    	tila = 1;  // TILA
    	//printf("\nTILA1");
      }
    }

    // Muut tilat paitsi puhdas base64-tiedosto
    if (tila != 1) {

      // Non-multipart
      
      // Non-multipart ja multipart
      // L�ytyyk� 'Content-type:' 
      if (content == -1){	  
	content = loytyykoContentType(input);
      }
      // Non-multipart: Tarkistetaan, onko base64-teksti t�ll� rivill�
      if( b64non == 0){
	b64non = loytyykoBase64(input);
      }


      // Multipart

      // *** Dekoodausalueen haku, kun sy�tteess� useita eri osia ***

      // Multipart-sy�tteen analysointi:

      // Tilamuuttujat etsiBo ja etsiB64 ohjaavat base64-enkoodatun
      // lohkon etsint�� multipart-viestist�.
      // etsiBo == 1, kun on sopiva tilanne etsi� erotinteksti�
      // sy�tteest�.
      // etsiB64 == 1, kun on sopiva tilanne etsi� 
      // 'Content-Type-Encoding: base64'-teksti� sy�tteest�.

      // Jos boundary-m��ritys on l�ytynyt sy�tteest� (boundaryLoytynyt = 1),
      // sallitaan erotintekstin etsiminen (etsiBo = 1)
      // ja paikannetaan erotinteksti (bo).
      // Kun erotinteksti on paikannettu (bo = 1), 
      // sallitaan 'C-T-E:base64'-tekstin etsiminen (etsiB64 = 1).
      // Jos ko. teksti l�ytyy ennen rivinvaihtoa (b64 = 1),
      // siirryt��n dekoodaukseen (de = 1).
      // Ellei 'C-T-E:base64'-teksti� l�ydy ennen tyhj�� rivi�,
      // siirryt��n takaisin erotintekstin paikantamiseen (etsiBo = 1)
      // Dekoodausalueen j�lkeen tyhj� rivi katkaisee multipart dekoodauksen 
      // ja siirt�� toiminnan takaisin erotintekstin etsimiseen (etsiBo = 1).
      
      // Katkaistaan dekoodaus (de -> 0), 
      // kun tulee tyhj� rivi dekoodausalueen j�lkeen
      // Huom. paikka t�rke� ennen dekoodausalueen mahd. aloittamista
      if( onkoTyhjaRivi(input) == 1 && de == 1){
	//printf("tyhj�, dekoodaus loppuu\n");
	de = 0;  // TILA
	bo = 0;
	b64 = 0;
	etsiBo = 1;  // sallii boundaryn etsinn�n
      }

      // L�ytyyk� multipart (Content-type l�ytynyt) 
      if( content != -1 && multi == 0){
	multi = loytyykoContentMulti(input, content);
      }

      // Etsit��n boundary-m��ritelm��, ellei ole viel� l�ytynyt
      if( boundaryLoytynyt == 0){
	boundaryLoytynyt = loytyykoBoundary(input, BOU);
	if(boundaryLoytynyt == 1){
	  etsiBo = 1;  // sallii erotintekstin etsimisen
	}
      }

      if( boundaryLoytynyt == 1 && etsiBo == 1){ 
	bo = paikannaBoundary(input, BOU);
	if(bo == 1){
	  etsiB64 = 1;  // sallii CTE:base64 -tekstin etsimisen
	  etsiBo = 0;   // est�� boundarytekstin etsimisen
	  b64 = 0;
	}
      }

      if( boundaryLoytynyt == 1){ 
	if(b64 == 0 && etsiB64 == 1){ 
	  b64 = loytyykoBase64(input);
	  if(b64 == 1){
	    etsiB64 = 0;  // est�� CTE:base64 -tekstin etsinn�n
	  }
	  if(onkoTyhjaRivi(input) == 1){
	    etsiB64 = 0;  // est�� CTE:base64 -tekstin etsinn�n
	    etsiBo = 1;   // sallii erotintekstin etsimisen
	  }
	} else if(b64 == 1){
	  if( onkoTyhjaRivi(input) == 1){
	    bo = 0;
	    b64 = 0;
	    de = 1;  // TILA
	  }
	} 
      }
      
      // Muuttujien tilatietoa      
      //printf("\n%d. boundaryLoytynyt = %d, bo = %d, b64 = %d, b64non = %d, de = %d, content = %d, multi = %d, etsiB64 = %d, etsiBo = %d\n", rr++, boundaryLoytynyt, bo, b64, b64non, de, content, multi, etsiB64, etsiBo);

      // *** Tilasiirtym�t, tila 2 ja tila 3 ***
      // Non-multipart
      if (content == 0 && multi == 0 && b64non == 1 \
	  && onkoTyhjaRivi(input) == 1 ){
	tila = 2;  // TILA
      }
      // Multipart
      if (multi == 1 ){
	tila = 3;  // TILA
      }

    } // if(tila != 1)


    switch(tila){

    case 1:  // Dekoodaus, puhdas base64

      // Tarkisto, ovatko kaikki merkit base64
      if(onkoRiviBase64(input, koodit, inputLkm) == 0){  //false
	// Virheilmoitus
	printf("\n\nTiedosto ei ollut pelkk�� base64-koodia.");
	printf(" Dekoodaus keskeytet��n.\n");
	exit(1);
      } else {
	//printf("\nTila 1, puhdas base64");
	kutsuDecode64(koodit, input, d, inputLkm);
      }
      break;
      
    case 2:  // Dekoodaus, ei multipart, base64

      // Skipataan tyhj� rivi
      if( onkoTyhjaRivi(input) != 1 ){
	// Tarkista, ovatko kaikki merkit base64
	if(onkoRiviBase64(input, koodit, inputLkm) == 0){  //false
	  // Virheilmoitus
	  printf("\n\nOsio ei ollut pelkk�� base64-koodia.");
	  printf(" Dekoodaus keskeytet��n.\n");
	  exit(1);
	} else {
	  //dekoodaus tied. loppuun saakka tyhji� lukuun ottamatta
	  kutsuDecode64(koodit, input, d, inputLkm);
	  //printf("\ntila 2, ei multipart, dekoodaus tied. loppuun saakka");
	}
      } else {
	//printf("\ntila 2, ei tehd� mit��n");
      }
      break;
      
    case 3:  // Dekoodaus, multipart, base64

      // Tilan 3 tunnistamisessa k�ytet��n multi-muuttujaa
      // Lis�ksi dekoodausta kontrolloidaan de-tilamuuttujalla
      // Tilamuuttuja de = 0, kun ei olla dekoodattavan rivin kohdalla
      // Kun tilamuuttuja de = 1, ollaan dekoodattavan alueen sis�ll�
      if( de == 1){
	//printf("\nTila 3, multipart, dekoodaus tiedoston loppuun saakka");
	// Tarkista, ovatko kaikki merkit base64
	if(onkoRiviBase64(input, koodit, inputLkm) == 0){  //false
	  // Virheilmoitus
	  printf("\n\nOsio ei ollut pelkk�� base64-koodia.");
	  printf(" Dekoodaus keskeytet��n.\n");
	  exit(1);
	} else {
	  //dekoodaus tyhj��n saakka
	  kutsuDecode64(koodit, input, d, inputLkm);
	}
      } else {
	//printf("\ntila3, ooooo ei dekoodausta");
      }
      break;
      
    case 4:  // Enkoodaus (bin.tied)
      kutsuEncode64(koodit, input, e, b, &y, &rivi, inputLkm);
      break;
      
    default:  // Ei tehd� mit��n (tavallinen teksti ilman 
      break;  // content-type� ja base64:aa)
      
    } //switch
  
    // Lopetusehto
    // enkoodaukselle: inputLkm > 0
    // dekoodaukselle: *input != EOF
  } while(inputLkm > 0 || *input != EOF);
    
  return (0);	
}

// ***********************************************************

// Luetaan rivi input streamista
// Lopettaa, kun tiedosto loppuu
// Enkoodauksen viimeinen, vajaa blokki kirjoitetaan t��ll�
// Enkoodauksessa luetaan fread()-funktiolla LINESIZE m��r� merkkej�
// Dekoodauksessa luetaan fgets()-funktiolla rivi kerrallaan

size_t lueRivi(char ** argv, char * koodit, unsigned char *input, \
int * b, int * y){
  size_t inputLkm = 0;

  if(strncmp(*(argv+1), "-d", 2) == 0){  //-d

    // Dekoodaus

    // Luetaan stdin:st� LINESIZE m��r� merkki�
    if(fgets((char *)  input, LINESIZE, stdin) == NULL){  //onko EOF
      //printf("\n%s","exit(1)\n\n");    
      exit(1);
    }
    inputLkm = annaInputKoko(input);

  } else if(strncmp(*(argv+1), "-e", 2) == 0){  //-e  

    // Enkoodaus

    if((inputLkm = fread(input, sizeof(char), LINESIZE, stdin)) == NULL){ 
      // Encode64():n lopetus '='-merkkeihin
      // Jos enkoodauksessa tavujen m��r� ei ole kolmella jaollinen
      // tulostetaan = -merkkej�, '==' kaksi tavua puuttuu, 
      // '=' yksi tavu puuttuu
      // Ennen =-merkkej� tulostetaan vajaaksi j��nyt base64-ryhm�
      if (*y == 2){  // puskurissa 2 merkki�, 1 puuttuu
	printf("%c%c%c", *(koodit+*b), *(koodit+*(b+1)), *(koodit+*(b+2)));
	printf("=");
      } else if (*y == 1){  // puskurissa 1 merkki, 2 puuttuu
	printf("%c%c", *(koodit+*b), *(koodit+*(b+1)));
	printf("==");
      }
      printf("\n");

      //printf("\n%s","exit(1)\n\n");    
      exit(1);
    }
  } 

  return (inputLkm); 
}

// Palauttaa taulukon koon
int annaKoko(char * taulu){
  int i;
  int pal = 0;
  char lop[] = {'\0'};
  int loppu = 0;
  for( i = 0; loppu == 0; i++){
    if(strncmp((taulu+i), lop, 1) == 0){
      pal = i;
      loppu = 1;
    }
  }
  return (pal);
}

// Palauttaa input-taulukon koon
size_t annaInputKoko(unsigned char * taulu){
  int i;
  size_t pal = 0;
  char ln[] = "\n";
  int loppu = 0;
  for( i = 0; loppu == 0; i++){
    if(strncmp((char *)(taulu+i), ln, 1) == 0){
      pal = i;
      loppu = 1;
    }
  }
  //  pal++;
  return (pal);
}

// Tarkistaa, onko koko rivi base64-koodeja
int onkoRiviBase64(unsigned char * input, char * koodit, size_t inputLkm){
  int vainBase64 = 1;  //true (optimistinen al.arvo)
  int inputKoko = 0;
  int i;
  int j;
  int k;
  unsigned char * input_i = 0;
  unsigned char * sis = 0;  

  // Onko koko rivi base64-koodattu

  for(i = 0; i < inputLkm && vainBase64 == 1; i++){
    k = 0;  // kierroslaskuri, jos == 64, ei l�yt�nyt koodia taulukosta 
            // -> ei b64
    
    for(j = 0; j < 64 && vainBase64 == 1; j++){
      if( strncmp( (char *) input+i, koodit+j, 1) == 0 || \
      	  strncmp( (char *) input+i, "=", 1) == 0 ){
      } else {
	k++;  // ei-laskuri
      }
      if(k >= 64){ // jos ei k�y kertaakaan koodit-taulukon puolella, k = 64, 
	// muutoin k = 63
	vainBase64 = 0;  //false
	//printf(" ei b64");
      }
    } 
  }
  //printf("\nvainBase64 = %d\n", vainBase64);

  // 1 = true, 0 = false
  return (vainBase64);
}


// Tarkistetaan, onko inputin ensimm�inen merkki newline '\n'
int onkoTyhjaRivi(unsigned char *input){
  int pal = 0;
  char ln[] = "\n";
  if(strncmp((char *) input, ln, 1) == 0){
    pal = 1;
    //printf("Tyhj� rivi\n");
  }
  // 1 = tyhj� rivi, 0 = ei ole tyhj�
  return (pal);
}


// L�ytyyk� 'Content-Transfer-Encoding: base64' -teksti t�lt� rivilt�
int loytyykoBase64(unsigned char *input){
  int pal = 0;
  int jatko = 0;
  int i, k, j;
  char cc[] = "C";
  char C[100];
  // C:n alustus
  for(i = 0; i < 100; i++){
    *(C+i) = 0;
  }
  char Cte[] = "CONTENT-TRANSFER-ENCODING:";

  // L�ytyyk� CTE-teksti inputista
  for(i = 0; ((i < LINESIZE) && (*(input+i)!='\0') && (jatko == 0)); i++){ 
    if(strncmp((char *) (input+i), cc, 1) == 0){
      k = i;
      j = 0;
      while(j < 26){
	if( isupper( *(input + k)) == 0){  // 0 = false
	  C[j++] = toupper( *(input + k++) );
	} else {
	  C[j++] = *(input + k++);
	}
      }
      C[j] = '\0';  //?
      if(strncmp(C, Cte, 26) == 0){  //onko "Content-transfer-encoding:"?
	//printf("\nsis�lt� oli: Content-transfer-encoding:");
	jatko = 1; 
	//ib = i;  //true = i  //ratkaisee, jatketaanko boundaryn etsint��
      }
    } 
  }

  // BASE64-sanan etsint�
  int jatko2 = 1;  //true, base64 ei ole l�ytynyt
  int n = 0;
  int m = 0;
  char b[] = "b";
  char B[] = "B";
  char BA[] = "BASE64";
  char C2[100];  //ty�tiedosto base64-tekstille
  if(jatko == 1){   //etsit��nk� base64 t�lt� rivilt�?
    for(m = 26; ((m < LINESIZE) && (*(input+m) != '\0') \
		 && (jatko2 == 1)); m++){
      if((strncmp((char *)(input+m), b, 1) == 0) || \
	  strncmp((char*)(input+m), B, 1) == 0){
	j = 0;
	k = m; // ?
	while(j < 6){
	  if( isupper( *(input + k)) == 0){  // 0 = false
	    C2[j++] = toupper( *(input + k++) );
	  } else {
	    C2[j++] = *(input + k++);
	  }
	}
	C2[j] = '\0';  
	//testaus, onko sana 'base64'
	if(strncmp(C2, BA, 6) == 0){
	  //printf("\nm = %d, base64 l�ytyi = ", m);
	  jatko2 = 0;  //base64-sana l�ytyi
	  pal = 1;  //base64 l�ytyi, funktion palautusarvo
	}
      
      } 
    }
  } 
  
  // 1 = base64 l�ytyi, 0 = ei l�ytynyt
  return (pal);  
}


int loytyykoContentType(unsigned char * input){
  int pal = -1;  // 'Content-type' l�ytyi = false, funktion palautusarvo
  char Ct[] = "CONTENT-TYPE:";  //sis. lopetusmerkin
  int i = 0;
  int m = 0;
  int l = 0;
  char cc[2] = "C";
  int j = 0, k = 0;
  char C[100];  //ty�taulu 'Content-type:'-tekstille
  // C:n alustus
  for(i = 0; i < 100; i++){
    *(C+i) = 0;
  }

  // Content-type: :n etsint�  
  for(i = 0; ((i < LINESIZE) && (*(input+i)!='\0') && (pal == -1)); i++){ 
    if(strncmp((char *)(input+i), cc, 1) == 0){
      k = i;
      j = 0;
      while(j < 13){
	if( isupper( *(input + k)) == 0){  // 0 = false
	  C[j++] = toupper( *(input + k++) );
	} else {
	  C[j++] = *(input + k++);
	}
      }
      C[j] = '\0';  
      if(strncmp(C, Ct, 13) == 0){  //onko "Content-type:"?
	//printf("\nsis�lt� oli: Content-type:\n");
	pal = i;
      }
    } 
  }

  // -1 ei l�ytynyt, muut arvot kertovat offsetin rivin alusta
  // sin�ns� turhaa, koska on kai aina 0
  return (pal);
}


int loytyykoContentMulti(unsigned char *input, int ib){
  int pal = 0;  // ei boundarya, funktion palautusarvo
  char Mul[] = "MULTIPART";
  int i = 0;
  int m = 0;
  int l = 0;
  char cc[2] = "C";
  int j = 0, k = 0;
  char C[100];  //ty�taulu 'Content-type:'-tekstille
  // C:n alustus
  for(i = 0; i < 100; i++){
    *(C+i) = 0;
  }
  char C2[100];  //ty�tiedosto 'boundary=' -tekstille
  char mu[] = "m";
  char MU[] = "M";

  //Multipart-sanan etsint�
  int jatko = 1;  //true, multipart ei ole l�ytynyt
  int n = 0;
  char h[] = "\"";  //heittomerkki
  char v[2];
  if(ib != -1){   //etsit��nk� multipart t�lt� rivilt�?
    for(m = ib; ((m < LINESIZE) && (*(input+ib) != '\0') \
		 && (jatko == 1)); m++){
      if((strncmp((char *)(input+ib+m), mu, 1) == 0) || \
	 strncmp((char *)(input+ib+m), MU, 1) == 0){
	j = 0;
	k = ib;
	
	while(j < 9){
	  if( isupper( *(input + m + k)) == 0){  // 0 = false
	    C2[j++] = toupper( *(input + m + k++) );
	  } else {
	    C2[j++] = *(input + m + k++);
	  }
	}
	C2[j] = '\0';  
	
	//testaus, onko sana 'multipart'
	if(strncmp(C2, Mul, 9) == 0){
	  //printf("\nm = %d, multipart l�ytyi = ", m);
	  jatko = 0;  //multipart-sana l�ytyi
	  pal = 1;    //funktion palautusarvo
	  int o = 0;
	}
	
      }
    }
  }
  // 0 = multipart ei l�ytynyt, 1 = l�ytyi
  return(pal);
}


// Buoundary-tekstin etsint�
// Ensin etsit��n rivi, joka alkaa sanalla: 'Content-type:'
// Jos sana l�ytyy, etsit��n b- tai B-kirjain ja luetaan sen j�lkeen 
// seuraavat merkit (yht. 9 kpl)
// Jos b-alkuinen sana on 'boundary=' tai 'BOUNDARY=', luetaan lainausmerkkien
// sis�lt� varsinainen boundary
// Muuttujat: char *input  rivi stdin-streamia (max 100 merkki�)
//            char *BOU    varsinainen erotinteksti, aliohjelma 'palauttaa'

int loytyykoBoundary(unsigned char *input, char *BOU){
  int pal = 0;  // ei boundarya, funktion palautusarvo
  char Ct[] = "Content-type:";  //sis. lopetusmerkin
  char Bou[] = "BOUNDARY=";  
  int i = 0;
  int m = 0;
  int l = 0;
  char cc[2] = "C";
  int j = 0, k = 0;
  int ib = -1;  //'Contenty-type:' l�ytyi = false
  char C[100];  //ty�taulu 'Content-type:'-tekstille
  // C:n alustus
  for(i = 0; i < 100; i++){
    *(C+i) = 0;
  }
  char C2[100];  //ty�tiedosto 'boundary=' -tekstille
  char b[] = "b";
  char B[] = "B";
  int jatko = 1;  //true, boundary ei ole l�ytynyt
  int n = 0;
  char h[] = "\"";  //heittomerkki
  char v[2];

  ib = loytyykoContentType(input);

  // Boundary-sanan etsint� ja boundaryn luenta
  if(ib != -1){   //etsit��nk� boundarya t�lt� rivilt�?
    for(m = ib+13; ((m < LINESIZE) && (*(input+ib) != '\0') \
		    && (jatko == 1)); m++){
      if((strncmp((char *)(input+ib+m), b, 1) == 0) || \
	 strncmp((char *)(input+ib+m), B, 1) == 0){
	j = 0;
	k = ib;

	while(j < 9){
	  if( isupper( *(input + m + k)) == 0){  // 0 = false
	    C2[j++] = toupper( *(input + m + k++) );
	  } else {
	    C2[j++] = *(input + m + k++);
	  }
	}
	C2[j] = '\0';  
	
	//testaus, onko sana 'boundary='
	if(strncmp(C2, Bou, 9) == 0){
	  jatko = 0;  //boundary-sana l�ytyi
	  int o = 0;

	  //etsit��n heittomerkki, pys�htyy kun l�ytyy
	  while(strncmp(v, h, 1) != 0){  
	    *v = *(input + ib + m + ++o);
	    //printf("\nMuu merkki kuin heittomerkki");
	  }

	  //luetaan varsinainen boundary
	  j = 0;
	  *v = *(input + ib + m + ++o);  //siirryt��n heittomerkin sis�lle
	  while(strncmp(v, h, 1) != 0){  //etsit��n 2. heittomerkki, 
	                                 //pys�htyy kun l�ytyy
	    BOU[j++] = *v;               //j�rjestys t�rke�: kirjoitus -> luku
	    *v = *(input + ib + m + ++o); 
	    //printf("\nVarsinaista boundarya");
	  }  
	  BOU[j] = '\0';
	  
	  if( j > 0){
	    pal = 1;  //boundary l�ytyi, funktion palautusarvo
	  }
	
	} 
      } 
    }
  }

  // 0 = ei l�ytynyt, 1 = l�ytyi  
  return(pal);
}


// L�ytyyk� erotinteksti t�lt� rivilt�
int paikannaBoundary(unsigned char * input, char * BOU){
  int pal = 0;
  char vii[] = "--";
  if(strncmp((char *) input, vii, 2) == 0){
    if(strncmp((char *) input+2, BOU, annaKoko(BOU)) == 0){
      pal = 1;
    }
  }
  return (pal);
}


//*************** ENKOODAUS ***********************

// Puskuroi input streamia ja l�hett�� kolme merkki� kerrallaan
// enkoodattavaksi
// lkm-muuttuja laskee, montako tavua j�i edelliselt� rivilt� enkoodaamatta

void kutsuEncode64(char * koodit, unsigned char * input, int *e, \
int * b, int *y, int * rivi, size_t inputLkm){
  int k = 0;  // indeksi, miss� kohtaa inputia ollaan menossa
  // y:n alkuarvo 3 asetetaan p��ohjelman alussa

  while (inputLkm > k){

    // Tarkista, onko puskurissa edellisen rivin dataa
    switch(*y){ // y = lkm puskurissa luettuja tavuja

    case 3:
      // Ei dataa valmiina puskurissa

      // Tarkistetaan, paljonko sy�tett� on j�ljell�
      // ja luetaan vain sen verran
      // Kolme tavua luetaan
      if(inputLkm >= k+3){
	*e = *(input + k++);
	*(e+1) = *(input + k++);
	*(e+2) = *(input + k++);
	*y = 3;
      } else if(inputLkm >= k+2){
	// Kaksi tavua luetaan
	*e = *(input + k++);   
	*(e+1) = *(input + k++);
	//(e+2) = *(input + k++);
	*y = 2;
      } else if(inputLkm >= k+1){
	// Yksi tavu luetaan
	*e = *(input + k++);
	//*(e+1) = *(input + k++);
	//*(e+1) = *(input + k++);
	*y = 1; 
      } 
      break;

    case 2:  
      // Kaksi tavua puskurissa

      // Tarkistetaan, paljonko sy�tett� on j�ljell�
      // Vain yksi tavu luetaan
      if(inputLkm >= k+1){
	//*e = *(input + k++);
	//*(e+1) = *(input + k++);
	*(e+2) = *(input + k++);
	*y = 3;
      } 
      break;

    case 1:
      // Yksi tavu puskurissa

      // Tarkistetaan, paljonko sy�tett� on j�ljell�
      // ja luetaan max kaksi tavua

      // Kaksi tavua luetaan
      if(inputLkm >= k+2){
	// Kaksi tavua luetaan
	//*e = *(input + k++);  
	*(e+1) = *(input + k++);
	*(e+2) = *(input + k++);
	*y = 3;
      } else if(inputLkm >= k+1){
	// Yksi tavu luetaan
	//*e = *(input + k++);
	*(e+1) = *(input + k++);
	//*(e+2) = *(input + k++);
	*y = 2; 
      } 
      break;

    default:
      break;
    }

    encode64(koodit, e, b, *y);

    // Rivinvaihto, max 76 merkki�
    if(*y == 3){
      *rivi = *rivi + 4;
    }
    if(*rivi >= 60){
      printf("\n");
      *rivi = 0;
    }
    
    // Mahdolliset yht�suuruusmerkit (=) kirjoitetaan
    // lueRivi()-aliohjelmassa

  } //while
}


// Kolme 8-bitin tavua yhdistet��n ja jaetaan nelj�ksi 6-bitin luvuksi.
// 6-bitin luvut muutetaan koodaustaulukon indeksien avulla base64-koodeiksi,
// jotka tulostetaan.
void encode64(char * koodit, int * e, int * b, int y){
  int i = 0;
  //int a = 0x0; //0x4d;  //01001101 = 77
  //int b = 0x0; //0x5c;  //01011100 = 92
  //int c = 0x0; //0x12;  //00010010 = 18
  int yhd = 0x0;
  int yhd2 = 0x0;
  int apu1 = 0x0;
  int apu2 = 0x0;
  int m1 = 0x0;
  int m2 = 0x0;
  int m3 = 0x0;
  int m4 = 0x0;
  int t = 0;  // laskuri, puuttuvien tavujen lkm

  // Yhdistet��n kolme tavua yhteen muuttujaan
  // j�rjestyksess� (MSB) a, b, c (LSB)
  yhd = 0x0;
  yhd = *e << 16;
  apu1 = *(e+1) << 8;
  apu2 = *(e+2);

  // Kirjoitetaan vain puskurissa olevat uudet merkit
  // *b-muuttujassa on t�ll�in oikeat base64-koodit my�s
  // kun viimeinen base64-blokki tulostetaan lueRivi()-aliohjelmasta 
  switch(y){
  case 3:  // kaikki tavut luettu
    yhd = yhd | apu1 | apu2;	// OR
    break;
  case 2:  // kaksi tavua luettu
    yhd = yhd | apu1;	// OR
    break;
  case 1:  // yksi tavu luettu
    //yhd = yhd;	// aika triviaalia, mutta yhten�ist�;)
    break;
  default:
    break;  
  }

  // Maskit
  m1 = 0xfc0000;	// 1-maski biteille 23-18
  m2 = 0x03f000;	// 1-maski biteille 17-12
  m3 = 0x000fc0;	// 1-maski biteille 11-6
  m4 = 0x00003f;	// 1-maski biteille 5-0
  
  *b = yhd & m1;	// AND
  *b = *b >> 18;	// siirto
  *(b+1) = yhd & m2;
  *(b+1) = *(b+1) >> 12;
  *(b+2) = yhd & m3;
  *(b+2) = *(b+2) >> 6;
  *(b+3) = yhd & m4;
  
  // Output stream
  // Normaalitilanne: kirjoitetaan 4 kpl, kun stream yh� jatkuu 
  // ja vain silloin kun y = 3
  if(y == 3){
    printf("%c%c%c%c", *(koodit+*b), *(koodit+*(b+1)), \
	   *(koodit+*(b+2)), *(koodit+*(b+3)));
  }
  // poikkeus: kun sy�te loppuu, pit�� viimeisell� kirjoituskerralla
  // kirjoittaa my�s mahd. vajaa ryhm� sek� '='-merkit 
  // -> siirretty vajaan ryhm�n kirjoitus lueRivi()-aliohjelmaan, miss�
  // my�s =-merkit kirjoitetaan
  
}


//******************** DEKOODAUS *************************

// Kontrollirakenne dekoodaukselle
void kutsuDecode64(char *koodit, unsigned char * input, char * d, \
size_t inputLkm){  
  int i = 0;
  int j = 0;
  int lkm = 0;
  int loytyi = 0;  //onko merkki base64-koodeja tai '='(1=true)
  int k = 0;

  while(inputLkm > k){
    
    // Luetaan stdin:st� tavuja, joissa 6 alinta 
    // bitti� merkitsevi�.
    // Luetaan nelj� merkki� kerrallaan dekoodaukseen (24 bit)
    if(inputLkm >= k+4){
      *d = *(input + k++);
      *(d+1) = *(input + k++);
      *(d+2) = *(input + k++);
      *(d+3) = *(input + k++);
    }
    
    // Tarkistetaan, ovatko nelj� merkki� base64-koodeja tai '='-merkki
    char *yh = "=";
    int yht = 0; 
    for(i = 0; i < 4; i++){
      loytyi = 0;
	for(j = 0; j < 64 && loytyi == 0; j++){
	  if(strncmp((d+i), (koodit+j), 1) == 0){
	    loytyi = 1;
	  } else if(strncmp(d+i, yh, 1) == 0){
	    yht++;;  // t�rke� jatkoa varten
	    loytyi = 1;
	  } 
	}
    }

    // Kutsutaan dekoodaus
    // (oikeiden koodien tarkistus p��ohjelman switch(tila)-rakenteessa)
    decode64(koodit, d, yht);

  }  //while
}

// Base64-dekoodaus
void decode64( char * koodit, char * d, int yht){   
  char o1 = 0;
  char o2 = 0;
  char o3 = 0;
  int yhd = 0x0;
  int yhd2 = 0x0;
  int apu1 = 0x0;
  int apu2 = 0x0;
  int apu3 = 0x0;
  int apu4 = 0x0;
  int a[] = {0x0, 0x0, 0x0, 0x0};
  int m1, m2, m3, m4;

  // Dekoodaus, 6-bitin arvot pit�� hakea base64-taulukosta, ei asciina
  // Jos base64-koodi t�sm��, otetaan taulukon indeksi numeroarvoksi
  int i, j;
  for(i = 0; i < 4; i++){
    for(j = 0; j < 64; j++){
      if(strncmp(d+i, koodit+j, 1) == 0){
	*(a+i) = j;
      }
    }
  }

  // Kirjoitetaan kaikki nelj� merkki� yhdistett�v��n bittijonoon
  // (toimii int *a:lla, ei char *d:lla)
  apu1 = *a << 18;  //lis�t��n ja siirret��n 6-bitti� MSB:ksi
  apu2 = *(a+1) << 12;
  apu3 = *(a+2) << 6;
  apu4 = *(a+3);
  yhd = 0x0;
  yhd = apu1 | apu2 | apu3 | apu4;  // OR, 24 bit

  // Maskit (ei ilm. tarpeen)
  m1 = 0xff0000;	// 1-maski biteille 23-16
  m2 = 0x00ff00;	// 1-maski biteille 15-9
  m3 = 0x0000ff;	// 1-maski biteille 8-0
  
  // Siirrot 
  yhd2 = yhd;
  yhd2 = yhd2 & m1;
  o1 = yhd2 >> 16;	
  yhd2 = yhd;
  yhd2 = yhd2 & m2;
  o2 = yhd2 >> 8;
  yhd2 = yhd;
  yhd2 = yhd2 & m3;
  o3 = yhd2;  

  //   printf("\napu1=%d,%d,%d,%d yhd=%d, o1=%d,%d,%d ",apu1,apu2,apu3,apu4,yhd,o1,o2,o3);
    
   switch(yht){
   case 0:
     printf("%c%c%c", o1, o2, o3);      
     break;
   case 1:
     printf("%c%c", o1, o2);      
     //printf("\nyksi =-merkkki\n");
    break;
   case 2:
     printf("%c", o1);      
     //printf("\nkaksi =-merkki�\n");
     break;
   default:
     break;
   }   

}


/*
void tulostaCharTaulu(char *t, int koko){
  int i;
  //printf("\n");
  for(i = 0; i < koko; i++){ 
    printf("%c",*(t+i));
  }
}
*/

