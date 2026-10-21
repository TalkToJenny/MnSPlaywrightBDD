Feature: Products

Verify that the products feature behaves as expected with various designed
scenarios that fully exercise its functionality.

@MSJT2111 @tag1
Scenario Outline: Verify that users can successfully add their intended product to their shopping bag
	Given User is already on their homepage
	And They select product category "<ProductCategory>" from the "<ProductSection>" product type
	And They filter products using filter category "<FilterCategory>" and by filter criteria "<FilterCriteria>"
	And The resulting result is sorted by "<SortCategory>"
	And User select the product "<ProductName>"
	And They choose the size "<Size>" for the product
	And User increase the product quantity to "<Quantity>"
	When User add the adjusted product to their shopping bag
	Then Product should successfully be added to customer shopping bag

Examples:
	| ProductSection | ProductCategory | FilterCategory | FilterCriteria | SortCategory   | ProductName                       | Size  | Quantity |
	| Home           | Rugs            | Pattern        | Plain          | Price Low-High | Zola Rug                          | Large |        1 |
	| Men            | Jeans           | Fit            | Slim fit       | Price High-Low | Bleecker Slim Fit 5 Pocket Jeans  |    34 |        3 |
	| Women          | All Shoes       | Colour         | Silver         | Price Low-High | Leather Crossover Strappy Sandals |     6 |        2 |
