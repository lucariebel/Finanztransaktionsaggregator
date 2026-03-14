

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

