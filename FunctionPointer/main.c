#include <stdio.h>
#include <stdlib.h>
#include "indicator.h"

int alarm1(int type){

	printf("Meltdown of type %i\n", type);
}
int alarm2(int type){

	printf("Meltdown of type %i\n", type);
}
int alarm3(int type){

	printf("Meltdown of type %i\n", type);
}

int main(){

	registerAlarm(alarm1);	
	registerAlarm(alarm2);	
	registerAlarm(alarm3);
	
	hitAlarm();
}
