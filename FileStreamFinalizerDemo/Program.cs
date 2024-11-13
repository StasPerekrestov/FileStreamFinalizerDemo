// See https://aka.ms/new-console-template for more information

using System.Runtime.CompilerServices;

Foo();
GC.Collect();
GC.Collect();
GC.Collect();
GC.WaitForPendingFinalizers();
GC.WaitForPendingFinalizers();
GC.WaitForPendingFinalizers();
GC.WaitForPendingFinalizers();
Console.WriteLine("Done");

[MethodImpl(MethodImplOptions.NoInlining)]
static void Foo()
{
    using (new DerivedFileStream("FileStreamFinalizerDemo.csproj"))
    {
    }

    new DerivedFileStream("FileStreamFinalizerDemo.csproj");
}

internal sealed class DerivedFileStream : FileStream
{
    private readonly string _path;

    internal DerivedFileStream(string path)
        : base(path, FileMode.Open)
    {
        _path = path;
    }

    ~DerivedFileStream()
    {
        Console.WriteLine($"Finalized {_path}");
    }
}