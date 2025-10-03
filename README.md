# R5A08 - TP1 API REST + Blazor

## 👤 Auteur
- **Nom :** Tsanev  
- **Prénom :** Pavel  

---

## 🚀 Description du projet
Ce projet est une application complète composée de :
- Une **API REST en ASP.NET Core 8** permettant la gestion des produits, marques et types de produits (CRUD).
- Un **front-end en Blazor** servant d’interface utilisateur pour afficher et manipuler les données.
- Une **base de données PostgreSQL** utilisée comme base.
- Une **batterie de tests unitaires (mock) et E2E** pour garantir la qualité du code (J'ai essayé les tests E2E j'ai pas réussi donc je n'ai rien fait).

---

## 🛠️ Dépendances principales

### API (`R5A08_TP1`)
- [.NET 8 SDK](https://dotnet.microsoft.com/download)  
- `Microsoft.EntityFrameworkCore.Tools` (v8.0.11)  
- `Npgsql.EntityFrameworkCore.PostgreSQL` (v8.0.11)  
- `Swashbuckle.AspNetCore` (v6.6.2) → Swagger UI  
- `AutoMapper.Extensions.Microsoft.DependencyInjection` (v12.0.1)  

### Tests (`R5A08_TP1Tests`)
- `MSTest.TestFramework` (v3.6.4)  
- `MSTest.TestAdapter` (v3.6.4)  
- `Microsoft.NET.Test.Sdk` (v17.12.0)  
- `Moq` (v4.20.72)  
- `Microsoft.AspNetCore.Mvc.Testing` (v8.0.11)  
- `Newtonsoft.Json` (v13.0.4)  

## Front Blazor (`BlazorApp`)
- Framework **Blazor Server** (.NET 8)  
- Bootstrap 5  
- Toast notifications custom  

---

## 🗄️ Base de données

Le projet utilise **PostgreSQL**.  
Configuration par défaut (dans `EntityFramework/ProductsDbContext`) : 

`protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("Server=localhost;port=5432;Database=R508; uid=postgres;password=postgres;");`


## 🗄️ Insération de données dans la base de données

La base de données est créeé par un script SQL a mettre sur pgadmin4
```sql
-- ==============================
-- SCRIPT D'INITIALISATION COMPLET
-- Produits avec images stables et fonctionnelles
-- ==============================

-- ==============================
-- NETTOYAGE DES ANCIENNES DONNÉES
-- ==============================
-- Suppression dans l'ordre pour respecter les contraintes FK
TRUNCATE TABLE product CASCADE;
TRUNCATE TABLE brand CASCADE;
TRUNCATE TABLE type_product CASCADE;

-- Réinitialisation des séquences (si vous utilisez des SERIAL)
ALTER SEQUENCE IF EXISTS product_id_product_seq RESTART WITH 1;
ALTER SEQUENCE IF EXISTS brand_id_brand_seq RESTART WITH 1;
ALTER SEQUENCE IF EXISTS type_product_id_type_product_seq RESTART WITH 1;

-- ==============================
-- SEEDING TYPE_PRODUCT
-- ==============================
INSERT INTO type_product (id_type_product, name_type_product) VALUES
(1, 'Smartphone'),
(2, 'Ordinateur'),
(3, 'Console'),
(4, 'TV'),
(5, 'Electroménager'),
(6, 'Vêtements'),
(7, 'Accessoires')
ON CONFLICT (id_type_product) DO NOTHING;

-- ==============================
-- SEEDING BRAND
-- ==============================
INSERT INTO brand (id_brand, name_brand) VALUES
(1, 'Apple'),
(2, 'Samsung'),
(3, 'Google'),
(4, 'OnePlus'),
(5, 'Dell'),
(6, 'HP'),
(7, 'Lenovo'),
(8, 'Sony'),
(9, 'Microsoft'),
(10, 'Nintendo'),
(11, 'LG'),
(12, 'Dyson'),
(13, 'Thermomix'),
(14, 'Nespresso'),
(15, 'Nike'),
(16, 'Adidas'),
(17, 'Levi''s'),
(18, 'North Face'),
(19, 'Fitbit'),
(20, 'Ray-Ban')
ON CONFLICT (id_brand) DO NOTHING;

-- ==============================
-- SEEDING PRODUCT avec images stables
-- URLs testées et fonctionnelles
-- ==============================
INSERT INTO product 
(name_product, description, name_photo, uri_photo, id_type_product, id_brand, actual_stock, min_stock, max_stock) 
VALUES
-- ========== SMARTPHONES ==========
('iPhone 15 Pro', 'Dernier modèle Apple avec puce A17 Pro', 'iphone15pro.jpg',
 'https://images.unsplash.com/photo-1695048133142-1a20484d2569?w=800&q=80',
 1, 1, 50, 5, 100),

('Samsung Galaxy S24', 'Flagship Samsung avec écran AMOLED', 'galaxys24.jpg',
 'https://images.unsplash.com/photo-1610945415295-d9bbf067e59c?w=800&q=80',
 1, 2, 40, 5, 80),

('Google Pixel 8', 'Smartphone Google avec Android pur', 'pixel8.jpg',
 'https://images.unsplash.com/photo-1598327105666-5b89351aff97?w=800&q=80',
 1, 3, 30, 5, 60),

('OnePlus 11', 'Rapide et fluide avec OxygenOS', 'oneplus11.jpg',
 'https://images.unsplash.com/photo-1511707171634-5f897ff02aa9?w=800&q=80',
 1, 4, 20, 5, 50),

-- ========== ORDINATEURS ==========
('MacBook Pro 16"', 'MacBook Pro avec puce M3 Max', 'macbookpro16.jpg',
 'https://images.unsplash.com/photo-1517336714731-489689fd1ca8?w=800&q=80',
 2, 1, 15, 2, 30),

('Dell XPS 13', 'Ultrabook compact et puissant', 'xps13.jpg',
 'https://images.unsplash.com/photo-1593642632823-8f785ba67e45?w=800&q=80',
 2, 5, 25, 3, 40),

('HP Spectre x360', 'Convertible tactile avec Windows 11', 'spectre.jpg',
 'https://images.unsplash.com/photo-1588872657578-7efd1f1555ed?w=800&q=80',
 2, 6, 18, 2, 30),

('Lenovo ThinkPad X1', 'Ordinateur pro robuste', 'thinkpadx1.jpg',
 'https://images.unsplash.com/photo-1525547719571-a2d4ac8945e2?w=800&q=80',
 2, 7, 12, 2, 25),

-- ========== CONSOLES ==========
('PlayStation 5', 'Console nouvelle génération Sony', 'ps5.jpg',
 'https://images.unsplash.com/photo-1606813907291-d86efa9b94db?w=800&q=80',
 3, 8, 60, 10, 100),

('Xbox Series X', 'Console Microsoft 4K HDR', 'xboxseriesx.jpg',
 'https://images.unsplash.com/photo-1621259182978-fbf93132d53d?w=800&q=80',
 3, 9, 55, 10, 90),

('Nintendo Switch OLED', 'Console hybride avec écran OLED', 'switcholed.jpg',
 'https://images.unsplash.com/photo-1578303512597-81e6cc155b3e?w=800&q=80',
 3, 10, 70, 10, 120),

-- ========== TV ==========
('LG OLED C3', 'TV OLED 55 pouces 4K HDR', 'lgc3.jpg',
 'https://images.unsplash.com/photo-1593359677879-a4bb92f829d1?w=800&q=80',
 4, 11, 25, 2, 40),

('Samsung Neo QLED', 'TV 65 pouces QLED 8K', 'samsungneo.jpg',
 'https://images.unsplash.com/photo-1593359677879-a4bb92f829d1?w=800&q=80',
 4, 2, 30, 3, 50),

('Sony Bravia XR', 'TV haut de gamme avec Google TV', 'braviaxr.jpg',
 'https://images.unsplash.com/photo-1461151304267-38535e780c79?w=800&q=80',
 4, 8, 20, 2, 35),

-- ========== ELECTROMENAGER ==========
('Dyson V15', 'Aspirateur sans fil intelligent', 'dysonv15.jpg',
 'https://images.unsplash.com/photo-1558317374-067fb5f30001?w=800&q=80',
 5, 12, 40, 5, 60),

('Thermomix TM6', 'Robot cuiseur multifonctions', 'thermomix.jpg',
 'https://images.unsplash.com/photo-1585515320310-259814833e62?w=800&q=80',
 5, 13, 18, 2, 30),

('Nespresso Vertuo', 'Machine à café automatique', 'nespresso.jpg',
 'https://images.unsplash.com/photo-1517668808822-9ebb02f2a0e6?w=800&q=80',
 5, 14, 30, 5, 50),

-- ========== VETEMENTS ==========
('Nike Air Force 1', 'Chaussures blanches classiques', 'airforce1.jpg',
 'https://images.unsplash.com/photo-1549298916-b41d501d3772?w=800&q=80',
 6, 15, 100, 10, 200),

('Adidas Ultraboost', 'Chaussures running confortables', 'ultraboost.jpg',
 'https://images.unsplash.com/photo-1542291026-7eec264c27ff?w=800&q=80',
 6, 16, 80, 8, 150),

('Levi''s 501', 'Jean coupe droite iconique', 'levis501.jpg',
 'https://images.unsplash.com/photo-1542272454315-7f6fabf0b87f?w=800&q=80',
 6, 17, 70, 7, 120),

('North Face Doudoune', 'Doudoune chaude pour l''hiver', 'northface.jpg',
 'https://images.unsplash.com/photo-1551028719-00167b16eac5?w=800&q=80',
 6, 18, 50, 5, 90),

-- ========== ACCESSOIRES ==========
('Apple Watch Series 9', 'Montre connectée Apple', 'applewatch9.jpg',
 'https://images.unsplash.com/photo-1434493789847-2f02dc6ca35d?w=800&q=80',
 7, 1, 40, 5, 60),

('Samsung Galaxy Watch 6', 'Montre connectée Samsung', 'galaxywatch6.jpg',
 'https://images.unsplash.com/photo-1579586337278-3befd40fd17a?w=800&q=80',
 7, 2, 35, 5, 55),

('Fitbit Charge 6', 'Tracker fitness avancé', 'fitbitcharge6.jpg',
 'https://images.unsplash.com/photo-1575311373937-040b8e1fd5b6?w=800&q=80',
 7, 19, 25, 5, 40),

('Ray-Ban Aviator', 'Lunettes de soleil iconiques', 'raybanaviator.jpg',
 'https://images.unsplash.com/photo-1511499767150-a48a237f0083?w=800&q=80',
 7, 20, 22, 3, 50),

('AirPods Pro 2', 'Écouteurs sans fil Apple avec réduction de bruit', 'airpodspro2.jpg',
 'https://images.unsplash.com/photo-1606841837239-c5a1a4a07af7?w=800&q=80',
 7, 1, 60, 5, 100),

('Sony WH-1000XM5', 'Casque Bluetooth avec ANC', 'sonywh1000xm5.jpg',
 'https://images.unsplash.com/photo-1546435770-a3e426bf472b?w=800&q=80',
 7, 8, 30, 5, 70),

('Logitech MX Master 3S', 'Souris ergonomique haut de gamme', 'mxmaster3s.jpg',
 'https://images.unsplash.com/photo-1527864550417-7fd91fc51a46?w=800&q=80',
 7, 5, 28, 2, 50)
ON CONFLICT DO NOTHING;

-- ==============================
-- VÉRIFICATION
-- ==============================
SELECT 
    COUNT(*) as total_products,
    COUNT(DISTINCT id_type_product) as total_types,
    COUNT(DISTINCT id_brand) as total_brands
FROM product;

-- Afficher tous les produits avec leurs images
SELECT 
    p.name_product,
    b.name_brand,
    t.name_type_product,
    p.uri_photo,
    p.actual_stock
FROM product p
JOIN brand b ON p.id_brand = b.id_brand
JOIN type_product t ON p.id_type_product = t.id_type_product
ORDER BY t.name_type_product, b.name_brand;
```

---

## Lancement du projet

Dezippez l'archive et ouvrez le dossier ou cloner le repo git directement.

git clone https://github.com/Whitos/R5A08_TP1.git

## ✅ Fonctionnalités principales

API:

- CRUD Produits
- CRUD Marques
- CRUD Types de Produits
- Swagger pour tester les endpoints

Blazor:

- Catalogue produits avec filtres et recherche (par mots, type, et marque) 
- Ajout, modification, suppression d’un produit
- Ajout d'une marque et d’un type de produit
- Overlay de détails produit
- Notifications Toast

Tests:

- Unit tests (Mock) : vérifient les contrôleurs via mocks (Products, Brands, TypeProducts)
- E2E tests : vérifient l’API en conditions réelles via WebApplicationFactory mais qui ne marche pas 

--- 

## 📌 Analyse SOLID de l’API REST

✅ Principes respectés

### 1. S–Single Responsibility Principle (Responsabilité unique)

Globalement respecté :
Les managers (ProductManager, BrandManager, TypeProductManager) s’occupent uniquement de la gestion des données (accès à la base).
Les contrôleurs (ProductsController, etc.) se concentrent sur l’exposition des endpoints REST.
Les DTOs (ProductDto, ProductCreateDto, etc.) sont séparés des entités EF, ce qui évite le couplage entre la base de données et l’API publique.

👉 Cela améliore la lisibilité et la maintenabilité : chaque classe a une responsabilité claire.

### 2. O–Open/Closed Principle (Ouvert/Fermé)

En partie respecté :
L’utilisation du pattern Repository (IDataRepository<T>) rend le code ouvert à l’extension (ajouter d’autres entités comme CategoryManager, SupplierManager, etc. est facile).
Mais il est fermé à la modification, car on ne touche pas aux classes existantes quand on ajoute une nouvelle entité.
Cela permet de faire évoluer le projet sans casser l’existant.

### 3. L–Liskov Substitution Principle

Respecté avec les interfaces :
Tout ProductManager, BrandManager, etc. peut être substitué par un mock (Mock<IDataRepository<Product>>) dans les tests, sans casser le code.
Le polymorphisme est correctement exploité via les interfaces (IDataRepository<T>).
Cela facilite les tests unitaires et le remplacement des dépendances.

### 4. I–Interface Segregation Principle

Le principe est plutôt bien respecté :
L’interface IDataRepository<T> ne contient que les méthodes nécessaires (GetAllAsync, GetByIdAsync, AddAsync, UpdateAsync, DeleteAsync).
Elle n’impose pas de méthodes inutiles aux classes concrètes.
Cela évite d’avoir des classes qui implémentent des méthodes dont elles n’ont pas besoin.

### 5. D–Dependency Inversion Principle

Partiellement respecté :
Les contrôleurs dépendent d’abstractions (IDataRepository<T>, IMapper) et non des implémentations concrètes.
En revanche, l’API dépend directement d’Entity Framework Core via les DbContext, ce qui est un point positif.
On pourrait introduire une couche de service métier entre les contrôleurs et les repositories pour séparer encore plus les responsabilités.
Actuellement, les dépendances sont inversées pour la partie repository, mais pas encore pour la logique métier.

--- 

## 🌟 Améliorations déjà appliquées

Séparation DTO / Entités EF → ca évite le couplage direct entre modèle interne et modèle exposé ainsi que la création d'un projet Shared afin de pas dupliquer les DTO dans l'API et le front.
Utilisation d’AutoMapper → simplifie la conversion entre entités et DTOs, améliore la maintenabilité.
Utilisation du pattern Repository → favorise les tests unitaires et respecte l’inversion de dépendance.
Mise en place de tests unitaires Mock et E2E → garantit la stabilité du projet, permet de détecter rapidement les régressions et voir aussi la sécurité.




