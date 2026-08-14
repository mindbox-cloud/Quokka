# Currency table generator

`Engine/Quokka.Core/Functions/Standard/Money/CurrencyFormats.g.cs` is generated from the
Unicode CLDR data vendored in `cldr/` — currency symbols from `currencies-en-001.json`,
minor units from `currencyData.json`, both copied verbatim from the release named in
`cldr/VERSION`.

Regenerate with Node and nothing else:

    node generate.mjs

CI runs the same command and fails if the committed file differs, so the table can be
neither edited by hand nor left behind when the data changes.

To take a newer CLDR release, replace the two files and the version, then regenerate:

    V=48.2.0
    curl -sfo cldr/currencies-en-001.json https://unpkg.com/cldr-numbers-full@$V/main/en-001/currencies.json
    curl -sfo cldr/currencyData.json      https://unpkg.com/cldr-core@$V/supplemental/currencyData.json
    echo $V > cldr/VERSION
    node generate.mjs

The two files are vendored rather than installed because the packages carrying them are
forty megabytes of every locale on earth, and this needs one locale and one supplemental
table. Vendoring also keeps the build off the network.

Every currency CLDR carries is generated, not a curated subset — the engine renders
templates for every integration, not just one, and a subset would silently downgrade any
currency a payment provider adds later. A code CLDR does not know falls back to
"amount + ISO code" at render time.
