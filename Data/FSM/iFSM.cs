using System;

public interface iFSM
{
	bool BUSY {get; set;}
	void Add(State e );
	
	void Set( int id );
	void Set( State e );
	void Set( Type t );
	void Next(  );
	
}