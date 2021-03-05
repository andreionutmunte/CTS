package ro.ase.acs.main;

import ro.ase.acs.readers.ConsoleReader;
import ro.ase.acs.services.Orchestrator;
import ro.ase.acs.writers.ConsoleWriter;
import ro.ase.acs.writers.Writeable;
import ro.ase.acs.readers.Readable;

public class Main {

	public static void main(String[] args) {
//		Orchestrator orchestrator =
//				new Orchestrator(new ConsoleReader(),
//						new ConsoleWriter());
		IoC container = new IoC();
		container.register(Readable.class, ConsoleReader.class);
	    container.register(Writeable.class, ConsoleWriter.class);
	    Orchestrator orchestrator = new Orchestrator(container);
		orchestrator.execute();
	}
}