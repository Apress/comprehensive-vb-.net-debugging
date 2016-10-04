class Test
{
	public static void Main()
	{
		Derived objDerived = new Derived();
		objDerived.DoSomething((long) 8);
		System.Console.ReadLine();
	}
}

class Base
{
	public virtual void DoSomething(long NewValue)
	{
		System.Console.WriteLine("Base:DoSomething(Long) called");
	}

	public void DoSomething(double NewValue)
	{
		System.Console.WriteLine("Base:DoSomething(Double) called");
	}
}

class Derived : Base
{
	override public void DoSomething(long NewValue)
	{
		System.Console.WriteLine("Derived:DoSomething(long) called");
	}

	public void DoSomething(int NewValue)
	{
		System.Console.WriteLine("Derived:DoSomething(Integer) called");
	}
}
