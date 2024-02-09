#language: fr
Fonctionnalité: Calcul du score d'une partie de bowling

Scénario: Le score sans strike ni spare est calculé correctement
Etant donné que on a fait les lancers suivants : <lancers>
Quand on calcul le score de la partie
Alors le score récupéré vaut l'addition de tous les lancers

Exemples: 
| lancers               |
| 0, 0                  |
| 1, 2                  |
| 5,4,2,1,5             |
| 8,0,1,2,5,1,3         |
| 0,9,0,9,1,4,5,1,2,3,1 |

Scénario: Le score avec des spares est calculé correctement
Etant donné que on a fait les lancers suivants : <lancers>
Quand on calcul le score de la partie
Alors le score récupéré vaut le score attendu : <score_attendu>

Exemples: 
| lancers               | score_attendu | description                     |
| 0, 10, 3              | 16            | simple spare avec lancer bonus  |
| 5, 5, 5, 5, 1, 2      | 29            | double spare avec lancers bonus |
| 5,5                   | 10            | simple spare sans lancer bonus  |

Scénario: Le score avec des strikes est calculé correctement
Etant donné que on a fait les lancers suivants : <lancers>
Quand on calcul le score de la partie
Alors le score récupéré vaut le score attendu : <score_attendu>

Exemples: 
| lancers      | score_attendu | description                        |
| 10           | 10            | simple strike sans lancer bonus    |
| 10, 3        | 16            | simple strike avec 1 lancer bonus  |
| 10, 3, 2     | 20            | simple strike avec 2 lancers bonus |
| 10, 10, 1, 2 | 37            | double strike avec 2 lancers bonus |