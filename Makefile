
RUNTIME = mono --debug

all: System.Reactive.Tests/bin/Debug/System.Reactive.Tests.dll

System.Reactive/bin/Debug/System.Reactive.dll:
	xbuild

System.Reactive.Tests/bin/Debug/System.Reactive.Tests.dll: System.Reactive/bin/Debug/System.Reactive.dll System.Reactive.Tests/*/*.cs
	xbuild

run-test: all
	$(RUNTIME) external/nunit26/nunit-console.exe System.Reactive.Tests/bin/Debug/System.Reactive.Tests.dll $(NUNIT_OPTIONS)
