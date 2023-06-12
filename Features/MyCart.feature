	Feature: MyCart
@mytag
Scenario: Add items to cart
	Given i add four random items to cart
	When  i view my cart
	Then  i found total four items listed in my cart
	When i serch for lowest price item
	Then i am able to remove the lowest item from my cart
	Then i am able to verify three items in my cart