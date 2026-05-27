# Directory Tracker
Directory Tracker is a simple application for tracking files and directories changes in a local file system. It provides a user-friendly interface to monitor changes, additions, and deletions of files and directories. The application is designed to be lightweight and efficient, making it suitable for both personal and professional use.

---
## Assigned Task
*Napište jednoduchý program, který bude umět detekovat změny v lokálnim adresáři uvedeném na vstupu. <br>
Při prvním spuštění si program obsah daného adresáře analyzuje a při každém dalšim spuštěni bude hlásit změny od svého posledniho spuštení, tj: <br>
	a) seznam nových souborů,<br>
	b) seznam změněných souborů (změnou se rozumí změna obsahu daného souboru).<br>
	c) seznam odstraněných souborü a podadresátů.<br>
U každého souboru evidujte číslo jeho aktuální verze (na začátku budou mít všechny soubory verzi 1, s každou detekovanou změnou daného souboru bude jeho verze navýšena o 1).<br>
Program realizujte jako jednoduchou ASP.NET aplikaci naprogramovanou v C#. Ul vytvořte jako webovou aplikaci dle své volby (Core MVC, MVC, REST API)<br>
Můžete předpokládat, že velikost souborů v adresáři bude do 50 MB a že počet souborů v každém adresáři bude nanejvyš 100.<br>
Program se bude spouštět ručně z Ul stiskem tlačítka (nedetekujte změny filesystému automaticky).<br>
Pro perzistenci dat nepoužívejte databázi.<br>
Ul bude obsahovat alespoň textbox (textový input) pro zadání cesty k analyzovanému adresáři, (tlačitko pro spuštění analýzy a výpis jejiho výsledku.<br>
Své řešení stručně popište a zmiňte i jeho případná omezení.*<br>

---

## :rocket: What is Directory Tracker?
- A tool to monitor changes in files and directories.
- Web-based interface for easy access and management.
- Stores all tracking data in your local file system, ensuring privacy and security.
---

## :building_construction: Architecture
- Backend: ASP.NET Core MVC (.NET 10)
- Frontend: Razor Pages with Bootstrap for styling
---

## :book: Documentation
[The documentation for Directory Tracker](./Docs/Development_Deployment_Guide.md) is available in the `docs` folder of the repository. It includes detailed instructions on how to set up, configure, and use the application effectively.

---

## :exclamation: Application Limitations
The following limitations reflect the current state of the application.
Developers should be aware of these constraints before contributing.

### Storage
- No persistent storage — all tracking data is held in json files on the local file system
- No database integration; data cannot be queried, filtered, or retained across sessions

### Security
- No user authentication or authorization
- No role-based access control (RBAC)
- All endpoints are publicly accessible
- Secrets and configuration values are not protected (no secret management integration)
- No CORS policy — cross-origin requests are unrestricted

### Reliability & Observability
- No structured logging or monitoring in place
- No health check endpoints
- No error tracking or alerting

### Performance
- No caching strategy implemented
- No rate limiting — the application is vulnerable to abuse under load
- File system tracking is polling-based; no real-time change detection (File System Watcher not implemented)

### Scalability
- Monolithic structure with no separation of concerns between layers
- Not designed for horizontal scaling — in-memory state would diverge across instances
- No CI/CD pipeline; deployments are manual

### Testing
- No unit, integration, or end-to-end tests present
- No test project scaffolded in the solution

---

## :computer: Authors

- **Long Jan Phan** (@honzaphan) – Architect and Lead Developer