Then(/^passing step$/) do
  # Nothing to be done here
end

Then(/^the scenario with danish characters like æøå and ÆØÅ shall pass$/) do
  # Nothing to be done here
end

Then(/^inconclusive step$/) do
  pending # We want pending here
end

Then(/^failing step$/) do
  expect("true").to eql("false")
end
