module Currency

/// US dollar
[<Measure>] type USD
[<RequireQualifiedAccess>]
module USD =
    let fromDecimal (amt:decimal) = amt * 1m<USD>
    let toDecimal (amt:decimal<USD>) = amt / 1m<USD>
