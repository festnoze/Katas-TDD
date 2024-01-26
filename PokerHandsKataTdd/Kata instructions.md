**<mark>Kata pour pratiquer le TDD</mark>**

**But**

Le but est de permettre de comparer deux mains de poker et indiquez laquelle, le cas échéant, est la main gagnante et pourquoi.

**Règles du Poker** Un jeu de poker Hold'em contient 52 cartes - chaque carte a une couleur qui est l'une des suivantes : trèfle, carreau, cœur ou pique (notées C, D, H et S dans les données fournies). Chaque carte a également une valeur qui est l'une des suivantes : 2, 3, 4, 5, 6, 7, 8, 9, 10, valet, dame, roi, as (notées : *2, 3, 4, 5, 6, 7, 8, 9, T, J, Q, K, A*). Aux fins du classement, les couleurs ne sont pas ordonnées tandis que les valeurs le sont comme indiqué ci-dessus, 2 étant la valeur la plus basse et l'as la plus haute.

Une main de poker se compose de : 5 cartes communes à tous + 2 cartes privées au joueur. Le but est d'avoir la meilleure combinaison en utilisant **uniquement 5 cartes** parmi les 7. 

**Liste des mains de poker :** (classées du plus faible au plus fort)

**Carte Haute** : Les mains qui ne correspondent à aucune catégorie supérieure sont classées par la valeur de leur carte la plus haute. Si les cartes les plus hautes ont la même valeur, les mains sont classées par la valeur suivante la plus élevée, et ainsi de suite.

**Paire** : 2 des 5 cartes dans la main ont la même valeur. Les mains qui contiennent toutes deux une paire sont classées par la valeur des cartes formant la paire. Si ces valeurs sont identiques, les mains sont classées par les valeurs des cartes ne formant pas la paire, dans un ordre décroissant.

**Deux Paires** : La main contient 2 paires différentes. Les mains qui contiennent toutes deux 2 paires sont classées par la valeur de leur paire la plus élevée. Les mains avec la même paire la plus élevée sont classées par la valeur de leur autre paire. Si ces valeurs sont identiques, les mains sont classées par la valeur de la carte restante.

**Brelan** : Trois des cartes dans la main ont la même valeur. Les mains qui contiennent toutes deux un brelan sont classées par la valeur des 3 cartes.

**Quinte** : La main contient 5 cartes avec des valeurs consécutives. Les mains qui contiennent toutes deux une quinte sont classées par leur carte la plus haute.

**Couleur** : La main contient 5 cartes de la même couleur. Les mains qui sont toutes deux des couleurs sont classées selon les règles de la Carte Haute.

**Full House** : 3 cartes de la même valeur, avec les 2 autres cartes formant une paire. Classé par la valeur des 3 cartes.

**Carré** : 4 cartes avec la même valeur. Classé par la valeur des 4 cartes.

**Quinte Flush** : 5 cartes de la même couleur avec des valeurs consécutives. Classé par la carte la plus haute de la main.

**Exemples** 

Joueur 1 (Bob) vs. Joueur 2 (John). Résultat :
2H 3D 5S 9C KD vs. 2C 3H 4S 8C AH Bob gagne - avec la carte haute : As
2H 4S 4C 2D 4H vs. 2S 8S AS QS 3S John gagne - avec un full house : 4 par2
2H 3D 5S 9C KD vs. 2C 3H 4S 8C KH John gagne - avec la carte haute : roi
2H 3D 5S 9C KD vs. 2D 3H 5C 9S KH Égalité

**Conseil** 

- Il y a deux dimensions à ce problème :
  
  - comment classer une main particulière ("Couleur" ou "Deux Paires", etc.)
  
  - comment comparer les mains et déterminer laquelle gagnera.

- Une façon très simple de commencer avec ce Kata est de se concentrer sur la première dimension et d'écrire beaucoup de code qui peut avec succès attribuer un rang à toutes les différentes sortes
