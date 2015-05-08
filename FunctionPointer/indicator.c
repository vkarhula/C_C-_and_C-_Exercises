#include "indicator.h"

void registerAlarm(alarm a){

   static int index = 0;
   // taulukkoon laitetaan funktioita
   p[index] = a;   
   index++;
   
}

int hitAlarm(){

   int i = 0;
   for(; i < 3; i++){
      p[i](i);
   }

}
