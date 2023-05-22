using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PanadaPpt
{
	internal class Program
	{
		static void Main(string[] args)
		{
			//instantiate class
			Panda joe = new Panda("Joe Biden");
			Console.WriteLine(joe);
			Panda donald = new Panda("Donald Trump");
			Console.WriteLine(donald);

			//Marry
			joe.Marry(donald);
			Console.WriteLine(joe);
			Console.WriteLine(donald);
		}


	}

	class Panda
	{
		private Panda mate;
		private string name;

		public Panda(string name)
		{
			this.name = name;
		}

		public void Marry(Panda mate)
		{
			this.mate = mate;
			mate.mate = this;
		}

		public override string ToString()
		{
			return $"{name} mate: {(mate != null ? mate.name : "Not Married")}";
		}
	}


}
