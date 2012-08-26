VERSION = 0.1.2
RUNTIME = mono --debug $(MONO_OPTIONS)

all: \
	System.Reactive.Tests2/bin/Debug/System.Reactive.Tests.dll \
	System.Reactive.Tests/bin/Debug/System.Reactive.Tests.dll

System.Reactive/bin/Debug/System.Reactive.dll:
	xbuild mono-reactive.sln || exit

System.Reactive.Tests/bin/Debug/System.Reactive.Tests.dll: System.Reactive/bin/Debug/System.Reactive.dll System.Reactive.Tests/*/*.cs
	xbuild mono-reactive.sln

System.Reactive.Tests2/bin/Debug/System.Reactive.Tests.dll: System.Reactive.Tests/*/*.cs
	xbuild mono-reactive2.sln

clean:
	xbuild mono-reactive2.sln /t:Clean
	xbuild mono-reactive.sln /t:Clean

run-test: all
	$(RUNTIME) external/nunit26/nunit-console.exe System.Reactive.Tests/bin/Debug/System.Reactive.Tests.dll $(NUNIT_OPTIONS)

run-test2: all
	$(RUNTIME) external/nunit26/nunit-console.exe System.Reactive.Tests2/bin/Debug/System.Reactive.Tests.dll $(NUNIT_OPTIONS)

dist:
	xbuild mono-reactive.sln
	xbuild mono-reactive2.sln

	mkdir -p Binaries
	mkdir -p Binaries/Rx1.0
	mkdir -p Binaries/Rx2.0

	cp System.Reactive/bin/Debug/*.dll* Binaries/Rx1.0
	cp System.Reactive.Providers/bin/Debug/*.dll* Binaries/Rx1.0
	cp Mono.Reactive.Testing/bin/Debug/*.dll* Binaries/Rx1.0

	cp System.Reactive.Linq/bin/Debug/*.dll* Binaries/Rx2.0
	cp System.Reactive.Providers2/bin/Debug/*.dll* Binaries/Rx2.0
	cp Mono.Reactive.Testing2/bin/Debug/*.dll* Binaries/Rx2.0

	xbuild mono-reactive.sln /p:Configuration=Debug /t:Clean
	xbuild mono-reactive2.sln /p:Configuration=Debug /t:Clean
	rm -rf System.Reactive/bin System.Reactive/obj
	rm -rf System.Reactive.Interfaces/bin System.Reactive.Interfaces/obj
	rm -rf System.Reactive.Core/bin System.Reactive.Core/obj
	rm -rf System.Reactive.PlatformServices/bin System.Reactive.PlatformServices/obj
	rm -rf System.Reactive.Linq/bin System.Reactive.Linq/obj
	rm -rf System.Reactive.Providers/bin System.Reactive.Providers/obj
	rm -rf System.Reactive.Providers2/bin System.Reactive.Providers2/obj
	rm -rf System.Reactive.Runtime.Remoting/bin System.Reactive.Runtime.Remoting/obj
	rm -rf Mono.Reactive.Testing/bin Mono.Reactive.Testing/obj
	rm -rf Mono.Reactive.Testing2/bin Mono.Reactive.Testing2/obj
	rm -rf System.Reactive.Tests/bin System.Reactive.Tests/obj
	rm -rf System.Reactive.Tests2/bin System.Reactive.Tests2/obj
	tar jcf mono-reactive-$(VERSION).tar.bz2 *
