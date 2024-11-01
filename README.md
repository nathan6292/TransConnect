# Projet TransConnect

Bienvenue dans le **Projet TransConnect** ! Ce document présente la structure, les fonctionnalités et les améliorations possibles de l'application TransConnect, développée pour optimiser les processus de gestion logistique et les ressources d'une entreprise de transport. Vous trouverez ici les détails des classes, de la gestion des données et des fonctionnalités proposées.

---

## 🔍 Vue d'ensemble

TransConnect est une application logistique conçue pour les entreprises de transport, facilitant la gestion des clients, des employés, des véhicules et des commandes. Le projet inclut une modélisation des entités du monde réel, des outils utilitaires pour la manipulation de données, et des fonctionnalités avancées comme la mise à jour en temps réel des trajets et la facturation automatisée.

---

## 📂 Structure du Projet

### Vue d'ensemble des Classes

#### Classes Principales

1. **Personne** *(abstraite)* : Représente une personne physique.
   - **Client** : Hérite de `Personne` et contient des informations spécifiques au client.
   - **Salarié** : Représente un employé de l’entreprise.
      - **Chauffeur** : Salarié spécialisé en transport de marchandises.

2. **Véhicule** *(abstrait)* : Modélise différents types de véhicules.
   - **Voiture** : Véhicule standard avec une capacité en nombre de places.
   - **Camionnette** : Utilisée pour le transport de marchandises, avec volume et type d’usage spécifiés.
   - **Poids Lourd** *(abstrait)* : Catégorie de véhicules lourds spécialisés.
      - **Camion Frigorifique**
      - **Camion Benne**
      - **Camion Citerne**

3. **Collection de Données** *(Liste Chainée)* :
   - `NoeudDico` : Contient une clé, une valeur et une référence au nœud suivant.
   - `DictionnaireChainé` : Liste chainée contenant le premier nœud et les fonctions utilitaires.

4. **Structure de Graphe** :
   - **Noeud** : Représente une ville avec ses coordonnées et la liste des arêtes.
   - **Arête** : Relie deux nœuds, avec des attributs de distance et de temps.
   - **Graphe** : Stocke les nœuds et fournit toutes les méthodes nécessaires.

5. **Commande** : Relie clients, chauffeurs, entreprise et véhicules, en contenant toutes les informations pertinentes pour une commande unique.

6. **Entreprise** : Gère toutes les ressources de l'entreprise, incluant clients, employés, véhicules et commandes.

7. **Tools** : Fonctions utilitaires pour assister les autres classes, notamment pour la validation des saisies utilisateur.

8. **Program.cs** : Menu principal et interface de navigation pour l'utilisateur.

9. **Interface IsToString** : Assure que les classes implémentent une méthode `ToString`.

---

## 📁 Stockage des Données

### Fichiers de Sauvegarde

Les données de l’entreprise sont enregistrées en fichiers CSV dans le dossier `Sauvegarde`, incluant toutes les informations sur les clients, véhicules, commandes, et employés. Ces fichiers sont lus au démarrage pour initialiser les objets, et sont mis à jour dès qu'une modification est effectuée, assurant ainsi la cohérence des données.

### Factures

Chaque commande génère une facture correspondante, stockée également dans le dossier `Sauvegarde`, avec une option d'envoi au client par e-mail.

---

## ✨ Fonctionnalités et Améliorations

### 1. Mise à Jour des Trajets en Temps Réel
   - L'application se connecte à l'API MapBox pour mettre à jour les distances entre villes en fonction du trafic, des conditions météorologiques et de l'heure de la journée, améliorant ainsi la précision des estimations de trajet.

### 2. Dictionnaire Chainé
   - Avec `NoeudDico`, le `DictionnaireChainé` fonctionne comme une liste chainée pour stocker et rechercher des données efficacement. Ce dictionnaire est notamment utilisé dans la fonction `Tools.Saisie` pour gérer les saisies utilisateur avec des conditions et des messages d’erreur personnalisés.

### 3. Génération Automatisée de Factures
   - Une fonction `CréerFacture` dans le module `Commande` permet de générer des factures PDF en récupérant les informations de la commande, du client et de l’entreprise.

### 4. Envoi de Factures par E-mail
   - En utilisant le protocole SMTP, l'application peut envoyer des factures en pièce jointe aux clients, facilitant ainsi la gestion des relations clients directement depuis la plateforme.

---

## 🎉 Démarrer avec le Projet

1. **Exécuter l'Application** : Naviguez via le menu principal dans `Program.cs`.
2. **Explorer les Classes** : Familiarisez-vous avec la structure des classes et leurs relations.
3. **Générer et Envoyer des Factures** : Profitez des fonctionnalités de création et d'envoi de factures pour simplifier vos opérations.

---

### 🚀 Merci d’avoir exploré TransConnect !
