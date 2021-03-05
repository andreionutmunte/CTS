package ro.ase.acs.main;

import java.util.HashMap;
import java.util.Map;

public class IoC {
	private Map<Class<?>, Class<?>> map = new HashMap();
	
	public void register(Class<?> contract, Class<?> implementation) {
		map.put(contract, implementation);
	}
	
	public <T> T resolve(Class<?> contract) {
		try {
			if(map.containsKey(contract)) {
				T newInstance = (T) map.get(contract).newInstance();
				return newInstance;			}
		} catch (InstantiationException | IllegalAccessException e) {
	            e.printStackTrace();
			}
		return null;
	}
}
