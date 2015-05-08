typedef int (*alarm)(int type);  //uuden funktiotyypin määrittely

int (*p[3])(int type);
//alarm p[3];    //toimii myös näin

void registerAlarm(alarm a);

int hitAlarm();
