/**
  * Version:0.0.1
  * Author: Virpi Karhula
  */

#include "operations.h"

/**
  * This is a main execution point of this application
  * @param argc number of command line arguments
  * @param argv pointer to array containing the cmd arguments
  * @return integer containing the exit status
  */
int main(int argc, char **argv){  // char *argv[]

   int ret_val = printEnvironment();  //tai ilman void ..();

   if(ret_val == 0){
      printOneEnvironment("HOME");
   }
   return 0;
}
