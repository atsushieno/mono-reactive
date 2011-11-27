all: System.Reactive.dll

System.Reactive.dll: */*.cs
	mcs */*.cs -t:library -out:System.Reactive.dll -debug

clean:
	rm System.Reactive.dll System.Reactive.dll.mdb */gen/*.exe
