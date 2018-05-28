using UnityEngine;
using System;
using System.Collections.Generic;

public class LiFSM : iFSM
{
	protected List<State> listaEstados;
	private State current;
	private State next;

	private bool ocupar;

	public bool BUSY 
	{
		set{ ocupar = value; }
		get{ return ocupar; }
	}

	public LiFSM()
	{
		listaEstados = new List<State>();

		current = new State();
		next = new State();
	}

	public void Add( State e )
	{

		if ( !listaEstados.Contains(e) ) 
		{
			e.parentFSM = this;
			listaEstados.Add( e );
			e.id = listaEstados.Count-1;
			//Debug.Log("added "+e+" id = "+e.id);

		}
	}

	public bool isCurrent( State e )
	{
		return ( e == current );
	}

	public bool isCurrent( Type t )
	{
		return ( t == current.GetType() );
	}

	public State GetCurrent( )
	{
		return current;
	}


	public void Set(int index)
	{
		next = listaEstados[index];

		if (next == null) 
		{
			Debug.Log( "Error, State " + index + " no encontrado");

			//			Debug.LogWarning( "Cambiando a State"+ listaEstados[0]+ " por defecto");
			//			EstablecerEstado( listaEstados[0] );
			return;
		}

		current.Off();
		current = next;

		current.On();

		/* Debug.Log("set " + current); */
	}

	/// utiliza solo la clase para establecerla
	public void Set( Type t )
	{
		//if ( BUSY ) return;

		next = FindState( t );

		if (next == null) 
		{
			Debug.Log( "Error, State "+t+ " no encontrado");

			//			Debug.LogWarning( "Cambiando a State"+ listaEstados[0]+ " por defecto");
			//			EstablecerEstado( listaEstados[0] );
			return;
		}

		current.Off();
		current = next;

		current.On();

		/* Debug.Log("set " + current); */
	}

	/// Busca y establece la instancia que recibe como parametro
	public void Set( State estd )
	{
		//if ( BUSY ) return;

		next = FindState( estd );

		if (next == null) 
		{
			Debug.Log( "Error, State "+ estd +" no encontrado");

			return;
		}

		current.Off();
		current = next;

		current.On();

		Debug.Log("set " + current);
	}


	internal State FindState(State e)
	{
		for (int i = 0; i < listaEstados.Count; i++)
		{
			if ( listaEstados[ i ] == e )
				return listaEstados[ i ];
		}

		return null;
	}


	internal State FindState(Type t)
	{
		for (int i = 0; i < listaEstados.Count; i++)
		{
			if ( listaEstados[ i ].GetType() == t )
				return listaEstados[ i ];
		}

		return null;
	}

	public void Update()
	{
		current.Update();
	}

	public void Next()
	{
		int nxtId = (current.id + 1) % listaEstados.Count;
		Debug.Log("next activated setting state w/ ID "+nxtId);
		Set(nxtId);

	}
}
