all: System.Reactive.Provider.dll

System.Reactive.Provider.dll: */*.cs
	mcs */*.cs -r:../System.Reactive/System.Reactive.dll -t:library -out:System.Reactive.Provider.dll -debug

clean:
	rm System.Reactive.Provider.dll System.Reactive.Provider.dll.mdb */gen/*.exe
