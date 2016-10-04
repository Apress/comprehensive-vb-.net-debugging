class Test
{
	public static void Main()
	{
		Derived objDerived = new Derived();
		MyTest objMyTest = new MyTest();
		objDerived.DoSomething(objMyTest);
		System.Console.ReadLine();
	}
}

class MyTest {}

class Base 
{
	public virtual void DoSomething(MyTest MyParameter)
	{
		System.Console.WriteLine("Base:DoSomething(MyTest) called");
	}
}

class Derived : Base
{
	public override void DoSomething(MyTest MyParameter)
	{
		System.Console.WriteLine("Derived:DoSomething(MyTest) called");
	}
	public void DoSomething(object MyParameter)
	{
		System.Console.WriteLine("Derived:DoSomething(Object) called");
	}
}
