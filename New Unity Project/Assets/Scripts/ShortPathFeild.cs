using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
	public int x,z,counter;
	public Node(int x,int z,int counter){this.x = x;this.z = z;this.counter = counter;}
}
public class Obsticle
{
	public int characterobjid;
	public int x,z,radius;
	public Obsticle(int x,int z,int radius){this.x = x;this.z = z;this.radius = radius;characterobjid = -1;}
	public Obsticle(int x,int z,int radius,int objid){this.x = x;this.z = z;this.radius = radius;characterobjid = objid;}
}

class Queue
{
	public Node[] arr;
	public int totalsize;
	public int front,last;

	public Queue(int size)
	{
		totalsize=size;
		front=0;last=-1;
		arr=new Node[totalsize];
	}
	public void reset()
	{
		front=0;last=-1;
	}
	public bool isFull()
	{
		return (last==totalsize-1);
	}
	public bool isEmpty()
	{
		return (front>last);
	}
	public void insert(int x,int z,int counter)
	{
		if(isFull())
			return;

		arr[++last]=new Node(x,z,counter);
	}
	public Node delete()
	{
		if(isEmpty())
			return null;
		Node tem=arr[front];
		arr[front++]=null;
		return tem;
	}
}

[System.Serializable]
public class Grid
{
	public int gridsizex,gridsizez;
	public Vector2Int goal;
	public int[,] arr;
	public Vector2[,] vectorfield;
	public List<Obsticle> obsticles;
	Queue list;

	public Grid(int sizex,int sizez)
	{
		gridsizex = sizex;
		gridsizez=sizez;
		arr=new int[gridsizez,gridsizex];
		obsticles = new List<Obsticle> ();
		vectorfield=new Vector2[gridsizez,gridsizex];
		list = new Queue (gridsizex*gridsizez*3);
	}

	public void reinitialize()
	{
		for (int z = 0; z < gridsizez; z++) {
			for (int z1 = 0; z1 < gridsizex; z1++) {
				arr [z,z1] = -1;
			}

		}
	}

	public bool isoutoofbound(int x,int z)
	{
		return !(x >= 0 && x < gridsizex && z >= 0 && z < gridsizez);
	}

	public void setObsticles()
	{
		foreach (Obsticle coordinate in obsticles)
			insertObsticle (coordinate);
	}

	public void insertObsticle(Obsticle obsticle)
	{
		for (int z = obsticle.z - obsticle.radius; z <= obsticle.z + obsticle.radius; z++)
			for (int x = obsticle.x - obsticle.radius; x <= obsticle.x + obsticle.radius; x++)
				if (!isoutoofbound (x, z)) 
					this.arr [z, x] = -2;

	}


	public void recalculateGrid(int goalx,int goalz)
	{
		list.reset();
		list.insert(goalx, goalz, 0);


		while (!list.isEmpty()) {
			Node temindex = list.delete();
			if (arr [temindex.z,temindex.x] != -1)
				continue;

			int x = temindex.x;
			int z = temindex.z;
			int counter = temindex.counter;
			arr [temindex.z,temindex.x] = temindex.counter;

			if (!isoutoofbound (x, z + 1) && arr [z + 1,x] == -1 )
				list.insert (x, z + 1, counter + 1);
			if (!isoutoofbound (x, z - 1) && arr [z - 1,x] == -1 )
				list.insert (x, z - 1, counter + 1);
			if (!isoutoofbound (x + 1, z) && arr [z,x + 1] == -1 )
				list.insert (x + 1, z, counter + 1);
			if (!isoutoofbound (x - 1, z) && arr [z,x - 1] == -1 )
				list.insert (x - 1, z, counter + 1);
		}

	}

	public Vector2 getVectorFeild(int z,int z1)
	{
		Vector2 dir = new Vector2 ();
		dir.x = dir.y = 0.0f;
		int minx = arr [z, z1],minz= arr [z, z1];

		if (!isoutoofbound (z1 + 1, z) && arr [z, z1 + 1] != -2 && arr [z, z1 + 1] < minx)
		{
			dir.x = 1.0f;
			minx = arr [z, z1 + 1];
		}
				
		if (!isoutoofbound (z1 - 1, z) && arr [z, z1 - 1] != -2 && arr [z, z1 - 1] < minx)
		{
			dir.x = -1.0f;
			minx = arr [z, z1 - 1];
		}

		if (!isoutoofbound (z1, z+1) && arr [z+1, z1] != -2 && arr [z+1, z1] < minx)
		{
			dir.y = 1.0f;
			minx = arr [z+1, z1];
		}
				
		if (!isoutoofbound (z1, z-1) && arr [z-1, z1] != -2 && arr [z-1, z1] < minx)
		{
			dir.y = -1.0f;
			minx = arr [z-1, z1];
		}

		if (dir != new Vector2 (0.0f, 0.0f))
			dir.Normalize ();
		return dir;
	}
}

public class ShortPathFeild :MonoBehaviour{

	public int sizex, sizez;
	public int goalx, goalz;
	public List<Obsticle> charaterobsticles;
	public int gridcount,gridupdatecounter;

	void Start()
	{
		charaterobsticles = new List<Obsticle>();
		gridcount =gridupdatecounter= 0;
	}

	public Grid initGrid()
	{
		gridcount++;
		return (new Grid (sizex,sizez));
	}

	public void reinit(Grid grid,int chracterid_callercharacter)
	{
		
		grid.reinitialize ();
		grid.setObsticles ();
		//add character obsticles
		foreach (Obsticle obsticle in charaterobsticles) {
			if(obsticle.characterobjid!=chracterid_callercharacter)
				grid.insertObsticle (obsticle);
		}
		grid.recalculateGrid (goalx,goalz);

		//truncate character obsticle list after all grids have been update
		if (++gridupdatecounter >= gridcount) {
			gridupdatecounter = 0;
			charaterobsticles.RemoveRange (0,charaterobsticles.Count);
		}
	}

	public Vector2 getVector(int x,int z,Grid grid)
	{
		if (!grid.isoutoofbound (x, z)) {
			return grid.getVectorFeild (z,x);
		}
		else
			return new Vector2 (0.0f,0.0f);
	}

	public bool isBlocking(Obsticle obsticle)
	{
		for (int z = obsticle.z - obsticle.radius; z <= obsticle.z + obsticle.radius; z++)
			for (int x = obsticle.x - obsticle.radius; x <= obsticle.x + obsticle.radius; x++)
				if (goalx == x && goalz == z)
					return true;
		
		return false;
	}

	public bool addCharacterObsticle(int x,int z,int radius,int characterobjid)
	{
		Obsticle obs=new Obsticle (x, z, radius, characterobjid);
		bool isblocking = isBlocking (obs);

		if(!isblocking)
			charaterobsticles.Add (obs);

		return isblocking;
	}

//	public void addObsticle(int x,int z,int weight)
//	{
//		if (!gridone.isoutoofbound (x, z)) {
//			gridone.obsticles.Add (new Obsticle (x,z,weight));
//		}
//	}

}
