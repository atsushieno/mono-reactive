
RUNTIME = mono --debug $(MONO_OPTIONS)

all: \
	System.Reactive.Tests/bin/Debug/System.Reactive.Tests.dll \
	System.Reactive.Tests2/bin/Debug/System.Reactive.Tests.dll

System.Reactive/bin/Debug/System.Reactive.dll:
	xbuild mono-reactive.sln || exit

System.Reactive.Tests/bin/Debug/System.Reactive.Tests.dll: System.Reactive/bin/Debug/System.Reactive.dll System.Reactive.Tests/*/*.cs
	xbuild mono-reactive.sln

System.Reactive.Tests2/bin/Debug/System.Reactive.Tests.dll: System.Reactive.Tests/*/*.cs
	xbuild mono-reactive2.sln

run-test: all
	$(RUNTIME) external/nunit26/nunit-console.exe System.Reactive.Tests/bin/Debug/System.Reactive.Tests.dll $(NUNIT_OPTIONS)

run-test2: all
	$(RUNTIME) external/nunit26/nunit-console.exe System.Reactive.Tests2/bin/Debug/System.Reactive.Tests.dll $(NUNIT_OPTIONS)
