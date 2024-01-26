Fonctionnalité: Calcul du score après une partie complète

Scénario: Calcul du score en renversant le même nombre de quilles à chaque lancer
    Soit je commence une nouvelle partie de bowling
    Quand je renverse <pinsDown> quilles pour chacun des <rollsCount> lancers
    Alors le calcul du score final fini en échec: non
    Et le score final devrait être <expectedScore>

Exemples:
| pinsDown | rollsCount | expectedScore |
| 0        | 20         | 0             |
| 2        | 20         | 40            |
| 5        | 21         | 150           |
| 10       | 12         | 300           |

Scénario: Calcul du score après un spare unique
    Soit je commence une nouvelle partie de bowling
    Quand je renverse <firstPinsDownCount> quilles
    Et je renverse <secondPinsDownCount> quilles
    Et je renverse <thirdPinsDownCount> quilles
    Et je ne renverse aucunes quilles lors des lancers suivants
    Alors le score final devrait être <awaitedScore>

    Exemples:
    | firstPinsDownCount | secondPinsDownCount | thirdPinsDownCount | awaitedScore |
    | 6                  | 4                   | 3                  | 16           |
    | 7                  | 3                   | 4                  | 18           |
    | 5                  | 5                   | 5                  | 20           |
    | 9                  | 1                   | 0                  | 10           |

Scénario: Calcul du score après un strike unique
    Soit je commence une nouvelle partie de bowling
    Quand je fais un strike
    Et je renverse <firstFollowingPinsDownCount> quilles
    Et je renverse <secondFollowingPinsDownCount> quilles
    Et je ne renverse aucunes quilles lors des lancers suivants
    Alors le score final devrait être <expectedScore>

  Exemples:
    | firstFollowingPinsDownCount | secondFollowingPinsDownCount | expectedScore |
    | 6                           | 3                            | 28            |
    | 7                           | 0                            | 24            |

Scénario: Calcul du score pour un spare après un strike
    Soit je commence une nouvelle partie de bowling
    Quand je fais un strike
    Et je renverse 7 quilles
    Et je renverse 3 quilles
    Et je renverse 4 quilles
    Et je ne renverse aucunes quilles lors des lancers suivants
    Alors le score final devrait être 38

Scénario: Vérification de l'état final du jeu
    Soit je commence une nouvelle partie de bowling
    Quand je joue les lancers suivants <rollResults>
    Alors le score final devrait être <expectedScore>

    Exemples:
    | rollResults                                                | expectedScore |
    | 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 | 0             |
    | 0, 1, 2, 3, 4, 5, 0, 1, 2, 3, 4, 5, 0, 1, 2, 3, 4, 5, 0, 1 | 46            |

Scénario: Tenter d'ajouter un lancer après la fin du jeu
    Soit je commence une nouvelle partie de bowling
    Quand je renverse <pinsDown> quilles pour chacun des <rolls> lancers
    Alors le calcul du score final fini en échec: <fails>

    Exemples:
    | pinsDown | rolls | fails |
    | 0        | 20    | non |
    | 2        | 20    | non |
    | 5        | 22    | oui  |
    | 10       | 13    | oui  |