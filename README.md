# Wildlife Sanctuary Dashboard

A platform designed for managing wildlife sanctuaries, tracking animal health, and monitoring conservation projects. The system supports six user roles, with different levels of access to features such as resource management, project creation, and notifications.

## Technologies Used

- **Backend**: C# (ASP.NET Core)
- **Frontend**: Angular
- **CSS Framework**: Tailwind CSS
- **Database**: SQL Server


## Features

### 1. **User Authentication and Role Management**
- **Roles**: Admin, Sanctuary Manager, Wildlife Biologist, Veterinarian, Ranger, Conservationist.
  - **All Users**: Have access to their personalized dashboards for tracking required information, such as resources, projects, and conservation activities.
- **Role-Based Access Control (RBAC)** to restrict actions based on roles.
  
### 2. **Sanctuary Management**
- Managers and Admins can register new sanctuaries, manage existing ones, and track sanctuary-related details such as location, protected areas, and conservation projects.
- **Sanctuaries Table** includes fields like `SanctuaryId`, `Name`, `Location`, `TotalArea`, and `Status`.

### 3. **Animal and Resource Management**
- **Animal Tracking**: Manage animal inventories, including species, health, and movement.
- **Resource Management**: Track expenses and quantity of resources such as animal feed, veterinary supplies, and equipment.
- **Resource Table** with fields like `ResourceId`, `Type`, `Quantity`, `StorageLocation`, and `LastRestockedDate`.

### 4. **Wildlife Data Management**
- Biologists can input and update wildlife observations, health reports, and behavioral data.
- Visualize records of animal movement and health.

### 5. **Project Management and Habitat Restoration**
- Create and manage conservation projects, including reforestation, species protection, and habitat restoration.
- Managers can track project timelines and project status.
- **Projects Table** includes fields like `ProjectId`, `SanctuaryId`, `ActivityType`, `StartDate`, and `AssignedRanger`.

### 6. **Incident and Safety Monitoring**
- Rangers can log safety incidents, such as poaching or human-wildlife conflicts, and track resolution status.
- Incident reports are stored in the database.

### 7. **Animal Health and Medical Records**
- Veterinarians can record and track health checkups, medical treatments, vaccinations, and emergency care.
- Add and manage the medical records easily.

### 8. **Cost and Budget Management**
- Track operational costs, including personnel, equipment, and project expenses.
- Budget management for each conservation project to monitor expenditures.


### 9. **Environmental Impact and Conservation Analysis**
- Maintain records on environmental health, climate conditions, and biodiversity assessments.
- Conservationist take care of managing the data such that new projects can be intiated for the betterment of sanctuary.

### 10. **Reporting and Analytics**
- Generate and view reports on various aspects, including wildlife health, project status, costs, and environmental impact.
- Analytics dashboards use **charts** to visualize data on animal health, population trends, and resource management.

### 11. **Notification System**
- Notifications are generated when critical events occur, such as resource shortage,project updates, or safety incidents.
- Notifications are displayed in the website.

### 12. **Multi-Platform Accessibility**
- The system is accessible on desktops, tablets, and mobile devices, enabling real-time updates and monitoring.
- A responsive UI adapts to various screen sizes, providing a seamless experience for both field staff and office personnel.


