# Currency table generator

`Engine/Quokka.Core/Functions/Standard/Money/CurrencyFormats.g.cs` is generated from the
Unicode CLDR data packages pinned in `package.json` — currency symbols from
`cldr-numbers-full`, minor units from `cldr-core`. It is committed so that builds stay
hermetic and every data change shows up as a reviewable diff.

To refresh after a CLDR release:

    npm ci
    npm run generate

CI runs the same two commands and fails if the committed file differs, so the table
cannot be edited by hand and cannot silently drift from CLDR.

`shopify-currencies.txt` lists the currency codes Shopify accepts as presentment
currencies; anything outside it falls back to "amount + ISO code" at render time.
