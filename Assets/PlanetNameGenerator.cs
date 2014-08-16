using UnityEngine;
using System.Collections;

/**
 * Slap together some syllables to generate some sci-fi sounding planet names
 */
public class PlanetNameGenerator : MonoBehaviour {
	
	static string[] titles = { 
		"New", "Proxima", 
		"Psi", "Alpha", "Beta", "Delta", "Gamma", 
		"Epsilon", "Sigma" };

	static string[] ordinals = { "I", "II", "III", "IV", "V", "Prime" };

	static string[] first = {
		"Hel", "Bal", "Can", "Mal", "Gal", 
		"Aven", "Far", "Fat", "Fur", "Ot", 
		"Gor", "Un", "Min", "Hel", "Khaf", 
		"Tar", "Vord", "Sol", "Avan", "Os",
		"Erm", "Avit", "Ber", "Uaf", "Att", 
		"Val", "Avit", "Gil", "Nord", "Sing",
		"Riv", "Delt", "Az", "Rek", "Ant", 
		"Mag", "Cat", "Lat", "Ekur", "Lan",
		"Oc", "Bak", "Om", "Carac", "Shon",
		"Phob", "Blerr", "Harmon", "Vent", "Max",
		"Lum", "Dor", "Lad", "Fid", "End",
		"God", "Gav", "Gald", "Hal", "Had",
		"Kev", "Cham", "Lor", "Mar", "Op",
		"Qat", "Rav", "Ral", "Rel"};

	static string[] second = {
		"ion", "zar", "ina", "erius", "ana",
		"tia", "ia", "alus", "aria", "ra",
		"una", "vos", "ania", "is", "ius", 
		"lon", "ria", "adria", "alla", "ataine",
		"lingen", "ara", "da", "iri", "asta",
		"icron", "icore", "ian", "eer", "etor",
		"eon", "cus", "ra", "ron"};

	public static string GenerateName() {

		bool hasPrefix = Random.Range(0,4) == 0;

		string title = "";
		if(hasPrefix) {
			title = titles[Random.Range(0, titles.Length-1)] + " ";
		}
		string name = first[Random.Range (0, first.Length-1)] + second[Random.Range (0, second.Length-1)];

		bool hasTwoWordName = Random.Range(0,5) == 0;
		string namePartTwo = "";
		if(hasTwoWordName) {
			namePartTwo = " " + first[Random.Range (0, first.Length-1)] + second[Random.Range (0, second.Length-1)];
		}

		bool hasOrdinal = Random.Range(0,3) == 0;

		string ordinal = "";
		if(hasOrdinal) {
			ordinal = " " + ordinals[Random.Range(0, ordinals.Length-1)];
		}

		return title + name + namePartTwo + ordinal;
	}
}
