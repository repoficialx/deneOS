# 🧠 deneOS  
![Versión](https://img.shields.io/badge/v0.2-beta-blue)  
![Compatible con Windows 11+](https://img.shields.io/badge/win11-compatible-darkgreen)  
![Empaquetado DPK](https://img.shields.io/badge/dpk-packaging-skyblue)

### 📌 ¿Qué es *deneOS*?

**deneOS** es un miniOS que se ejecuta sobre Windows.  
Ligero, rápido y personalizable, está diseñado para ofrecer una experiencia minimalista y directa, sin complicaciones ni distracciones.

### -- IMPORTANTE --
Desde el 4 de mayo de 2026 ya no hay ningún proyecto de deneOS que use .NET 8 ya que denePathParser ha sido actualizado a .NET 9. Ya no es obligatorio .NET 8 para contribuir o desarrollar.
**Pronto -estimado 22 de mayo- se terminará el cambio de todo a .NET 10 y ya no será necesario más .NET 9.**

### 💻 ¿Qué es un miniOS?

Un **miniOS** (mini operating system) es una aplicación que sustituye al shell de Windows pero viene con ecosistema de aplicaciones similar al de un sistema operativo real

Es ideal para:

- Computadoras dedicadas a tareas específicas  
- Proyectos experimentales  
- Entornos controlados o kioskos
- Ordenadores de menor rendimiento

En resumen: es como otro sistema pero cogiendo lo útil y bueno de Windows como sus aplicaciones.

### 🧩 Tecnologías usadas

- **.NET 8** -> Nada ya
- **.NET 9** -> denePathParser
- **.NET 10** -> aboutDialogs, controlcenter, Calendar.IO, deneOS, deneFiles, deneNavi, deneOS Launcher, deneStore, dpkxt, dpkxtconsole, Internet, setConfig, Terminal, WARun, deneNotes, DPKBundler, dosu, dosu.AI, dosu.System, dosu.UI, deneTerm.Core, deneAI.CLI y deneAI

### 📦 Aplicaciones, paquetes, instaladores y desarrollo

#### 📄 Documentación de tipos de aplicación Made for deneOS:

- *.dpk -> Paquete instalador diseñado por DPKBundler o manualmente, consiste en los archivos de programa con una carpeta 'meta' y dentro de esta, un 'manifest.json' con información de la aplicación

- *.dna -> Aplicación portable sin guardado interno. Puede ser autocontenida y sus datos de guardado deben realizarse de forma local en C:\DNUSR\Data\<nombre_app>\ o de forma de sistema en C:\SOFTWARE\GlobalSaveData\<nombre_app>\. De todas maneras se puede, aunque no se recomienda, guardar en (Usuario)\AppData, C:\ProgramData o similares

- *.wpi -> Archivo ejecutable de Windows (.exe) pero con la extensión cambiada a wpi, que indica compatibilidad con el antiguo miniOS 'Windoze' y se mantiene por compatibilidad legacy en deneOS.
> [!WARNING]
> Aviso -> *El soporte de .wpi será retirado en deneOS 1.0*

#### 🈸💼 Documentación de rutas y librerías de sistema.

- *denePathParser*
- - denePathParser es una simple librería para deneOS que permite convertir rutas en formato interno (C:\DNUSR\Documents) a externo (~\Documents) y viceversa. **Especialmente recomendado en apps con interfaz gráfica que muestren rutas de archivo**

- *dosu*
- - dosu es una librería completa para deneOS que permite acceder a funciones del sistema de deneOS y mucho más, al sistema con acceso a la configuración de conexión a internet, brillo de pantallas y más funciones

**- EXTENSIONES DE DOSU -**

*dosu.System*
- dosu.System es una extensión de dosu que permite acceder a utilidades más internas y del sistema. Solo usar si es necesario.

*dosu.UI*
- dosu.UI es una extensión de dosu para aplicaciones de winforms (.NET) con funciones de interfaz adaptadas a deneOS

*dosu.AI*
- dosu.AI es la extensión de IA para dosu con funciones de la IA local de deneOS (deneAI). Aún no está completamente terminado aunque ya soporta historial y la lista oficial de modelos compatibles con deneOS. Funciona completamente sin conexión.

**- RUTAS DE TIPO DENEOS -**

*Rutas de nivel deneOS*
- *Usuario*
- - Los archivos de usuario se almacenarán en C:\DNUSR\, que en estilo deneOS es ~\, y las aplicaciones no deben permitir salir de ahí con `..` ni nada similar.
- - Las carpetas generadas por defecto son Data, Desktop, Documents y Downloads
- *Software*
- - Todas las aplicaciones deberán estar guardadas en C:\SOFTWARE\, que en tipo deneOS es ~S\, y las aplicacioners solo pueden permitir salir si se activan con la flag `/dangerZone:enableRoot` o similares.
- *Sistema*
- - Esta ruta no debe ser ni visible ni accesible desde aplicaciones Made for deneOS de la deneStore. Solo si se trabaja con `/specialMode:accessSystem^Y` como flag. La ruta real es C:\DENEOS\.

> [!WARNING]  
> Solo se puede acceder a C:\DENEOS\ si el motivo está explícitamente documentado (la aplicación será revisada manualmente para verificar si hay alteraciones en la carpeta). NUNCA ninguna aplicación podrá modificar C:\DENEOS\core\. Si esta ruta es modificada, la aplicación será inmediatamente rechazada y/o eliminada de deneStore y se retirará el permiso de publicación.

**- RUTAS DE WINDOWS -**
Para tratar con rutas de Windows en aplicaciones para deneOS, solo si es realmente necesario mostrarlas, se prefiere que se muestre como ~W\. *Este tipo de ruta no es convertible con denePathParser y no es recomendada.*

### 🛠️ Próximamente

- 🎨 Temas personalizados y gestor de ventanas propio
- 🧩 Sistema modular para apps y ajustes

### ℹ️ Más información
Más información en [repoficialx: deneOS](https://repoficialx.xyz/deneOS) y en [repoficialx: Portal del desarrollador](https://repoficialx.xyz/profile/devportal)

### 🧪 Estado del proyecto

> **deneOS v1.0** estará disponible próximamente. (previsto para 2026 o 2027)
Actualmente se encuentra en fase **beta**, pero ya se puede probar su funcionalidad base.

---

**© 2026 Ray / repoficialx – Proyecto experimental, sin garantías.**
