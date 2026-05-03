

namespace FinanztransaktikonsaggregatorApp.Domain.Category;

public class CategoryService : ICategoryService 
{
    private readonly Dictionary<string, string[]> _categoryKeywords = new()
{
    { "Lebensmittel", new[]
        {
            "supermarkt","aldi","lidl","rewe","edeka","penny","netto",
            "kaufland","real","tegut","globus","lebensmittel","bäckerei",
            "baker","metzger","fleischer","obst","gemüse"
        }
    },

    { "Restaurant", new[]
        {
            "restaurant","cafe","café","bistro","pizzeria","pizza",
            "burger","imbiss","döner","mcdonald","burger king",
            "subway","lieferando","takeaway"
        }
    },

    { "Miete", new[]
        {
            "miete","warmmiete","kaltmiete","vermieter",
            "wohnungsbau","immobilien","hausverwaltung"
        }
    },

    { "Nebenkosten", new[]
        {
            "strom","gas","wasser","energie","stadtwerke",
            "heizung","wärme","utility"
        }
    },

    { "Internet & Telefon", new[]
        {
            "vodafone","telekom","o2","1&1","telefon",
            "internet","dsl","mobilfunk","handyvertrag"
        }
    },

    { "Streaming", new[]
        {
            "netflix","spotify","amazon prime","prime video",
            "disney","disney+","youtube premium","apple music"
        }
    },

    { "Shopping", new[]
        {
            "amazon","ebay","zalando","otto","etsy",
            "ikea","mediamarkt","saturn","online shop"
        }
    },

    { "Transport", new[]
        {
            "bahn","db","deutsche bahn","ticket","zug",
            "uber","bolt","taxi","bus","tram","verkehrsbetriebe"
        }
    },

    { "Auto", new[]
        {
            "tanken","tankstelle","shell","aral","esso",
            "jet","total","werkstatt","reifen","autohaus"
        }
    },

    { "Gesundheit", new[]
        {
            "apotheke","arzt","zahnarzt","krankenhaus",
            "medikament","medizin","praxis"
        }
    },

    { "Versicherung", new[]
        {
            "versicherung","allianz","axa","huk",
            "haftpflicht","kfz versicherung","versicherung ag"
        }
    },

    { "Freizeit", new[]
        {
            "kino","cinema","theater","museum",
            "freizeit","park","zoo","event","ticketmaster"
        }
    },

    { "Fitness", new[]
        {
            "fitness","gym","mcfit","fitx","clever fit",
            "fitnessstudio","sportstudio"
        }
    },

    { "Reisen", new[]
        {
            "hotel","booking","airbnb","flug","lufthansa",
            "ryanair","urlaub","reise","expedia"
        }
    },

    { "Bank", new[]
        {
            "gebühr","kontoführung","bankgebühr",
            "überweisung","lastschrift"
        }
    },
    { "Gehalt", new[]
    {
        "gehalt","lohn","salary","arbeitslohn","vergütung",
        "arbeitgeber","payroll","bonus","prämie"
    }
    },

    { "Bildung", new[]
        {
            "schule","universität","uni","hochschule","studium",
            "kurs","weiterbildung","udemy","coursera","seminar",
            "bücher","lehrbuch"
        }
    },

    { "Abos & Mitgliedschaften", new[]
        {
            "abo","abonnement","mitgliedschaft","membership",
            "patreon","onlyfans","verein","clubbeitrag",
            "jahresbeitrag","monatsbeitrag"
        }
    },

    { "Haushalt", new[]
        {
            "dm","rossmann","müller","drogerie","haushalt",
            "reinigung","putzmittel","waschmittel","möbel",
            "dekoration","baumarkt","obi","hornbach","bauhaus"
        }
    },

    { "Geschenke & Spenden", new[]
        {
            "geschenk","blumen","florist","spende","donation",
            "charity","hilfswerk","unicef","rotes kreuz",
            "geburtstag","weihnachten"
        }
    },
    { "Kinder & Familie", new[]
    {
        "kindergarten","kita","schule","schulessen","elternbeitrag",
        "kinderbetreuung","babyausstattung","spielzeug","baby",
        "windeln","familie","nachhilfe","hort","tagesmutter",
        "kinderarzt","familienkasse","kindergeld"
    }
    },

    { "Beauty & Pflege", new[]
        {
            "friseur","barber","haarschnitt","kosmetik","nagelstudio",
            "beauty","parfümerie","douglas","sephora","pflege",
            "hautpflege","makeup","make-up","drogerie","massage",
            "wellness","spa","solarium"
        }
    },

    { "Haustiere", new[]
        {
            "tierarzt","fressnapf","zooplus","zoo","tierbedarf",
            "hundefutter","katzenfutter","futterhaus","haustier",
            "hund","katze","tierklinik","impfung","tierapotheke",
            "hundesteuer","hundeschule"
        }
    },

    { "Steuern & Behörden", new[]
        {
            "finanzamt","steuer","steuern","einkommensteuer",
            "lohnsteuer","grundsteuer","kfz steuer","elster",
            "behörde","bürgeramt","einwohnermeldeamt","rathaus",
            "gebührenbescheid","verwaltung","amt","ausweis",
            "reisepass","zulassungsstelle"
        }
    },

    { "Kredite & Finanzierung", new[]
        {
            "kredit","darlehen","rate","ratenzahlung","finanzierung",
            "kreditkarte","visa","mastercard","zins","zinsen",
            "tilgung","hypothek","baufinanzierung","leasing",
            "klarna","paypal ratenzahlung","consors finanz",
            "santander","targobank"
        }
    },
    { "Gaming", new[]
        {
            "steam","playstation","xbox","nintendo","epic games",
            "gog","game pass","ps plus","gaming","videospiel",
            "riot games","blizzard","ea app","ubisoft"
        }
    },

    { "Software & Cloud", new[]
        {
            "microsoft","google cloud","aws","azure","dropbox",
            "icloud","github","gitlab","jetbrains","adobe",
            "canva","notion","openai","chatgpt"
        }
    },

    { "Gebühren & Strafen", new[]
        {
            "mahnung","mahngebühr","säumniszuschlag","bußgeld",
            "verwarnung","strafe","inkasso","rücklastschrift",
            "gebühr","service fee","penalty","late fee"
        }
    }


};

    public string GetCategoryForDescription(string description)
    {
        if (string.IsNullOrWhiteSpace(description))
            return "Uncategorized";

        foreach (var category in _categoryKeywords)
        {
            foreach (var keyword in category.Value)
            {
                if (description.Contains(keyword, StringComparison.OrdinalIgnoreCase))
                {
                    return category.Key;
                }
            }
        }

        return "Uncategorized";
    }
}

