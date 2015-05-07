INCLUDE=/include/
CC= gcc
CFLAGS= -Wall -I.
EXE_NAME= ENV
FILES= main.o operations.o

all:$(FILES)
	$(CC) -o $(EXE_NAME) $(FILES) $(CFLAGS)

clean:
	rm -f *.o

rebuild:clean all

run:
	./$(EXE_NAME)
