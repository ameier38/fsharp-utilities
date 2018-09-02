module Currency

/// US dollar
[<Measure>] type USD

let usd (amt:decimal) = amt * 1m<USD>
