using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Level {
	public class Generate : MonoBehaviour {
		private Area center;
		private Area west;
		private Area north;
		private Area south;
		private Area east;

		void Start() {
			center = gameObject.AddComponent<Area>();
			center.cardinal = "center";
			center.LoadAt(0, 0);

			//GenerateNeighbors();
		}

		void GenerateNeighbors() {
			GenerateNorth();
			GenerateSouth();
			GenerateEast();
			GenerateWest();
		}

		void GenerateEast() {
			east = gameObject.AddComponent<Area>();
			east.cardinal = "east";
			east.LoadAt(2, 0);
		}

		void GenerateWest() {	
			west = gameObject.AddComponent<Area>();
			west.cardinal = "west";
			west.LoadAt(-2, 0);
		}

		void GenerateNorth() {
			north = gameObject.AddComponent<Area>();
			north.cardinal = "north";
			north.LoadAt(0, 2);
		}

		void GenerateSouth() {
			south = gameObject.AddComponent<Area>();
			south.cardinal = "south";
			south.LoadAt(0, -2);
		}
	}
}