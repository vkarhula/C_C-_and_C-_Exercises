#include <stdio.h>
#include <stdlib.h>

int * createArray(int size);

int main(){

   int values[10];	// varaa paikat [0]...[9]
   values[3] = 12;
   printf("%i\n", values[3]);

   //values[10] = 2;   //kirjoittaa yli varatun muistialueen!
   //printf("%i\n", values[10]);

   int * myArray = createArray(10);
   myArray[0] = 12;
   printf("%i\n", myArray[0]);

   // mallocin varaaman muistin vapautus
   // pitää dokumentoida, kenen vastuulla on varatun heap-muistilohkon
   // vapauttaminen, alkuperäisen varauksen tekijällä vai sillä,
   // joka saa pointterin
   free(myArray);

   return 0;
}

int * createArray(int size){

   // malloc varaa muistia heap:sta
   int* array = (int*) malloc(sizeof(int) * size);
   return array;
}
