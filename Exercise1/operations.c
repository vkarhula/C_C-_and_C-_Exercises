#include <stdio.h>
#include <stdlib.h>

#define MAX 10		// ei voi käyttää const
int array[MAX];
const int MAX_LINES = 10;	//for(;;)

int printEnvironment(){

   char * pwd = getenv("PWD");
   if(pwd){
	printf("PWD is: %s\n", pwd);
	return 0;
   }
   return -1;
}

int printOneEnvironment(const char *env){ 
	//const muuttuja voidaan vain lukea, ei muuttaa!

   char *some_env = getenv(env);
   if(some_env){
      printf("The value is: %s\n", some_env);
      return 0;
   }
   return -1;

}
