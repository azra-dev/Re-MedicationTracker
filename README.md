# :pill: MediTrack
**MediTrack:** Keep your life on track.

A CPE106 Group Project dedicated for medication tracker program "MediTrack" using C# with WPF framework and with SQL database.

## Project Overview
The Medication Tracker app, or simply MediTrack, is designed to simplify the management of medication schedules and promote adherence to prescribed treatments. With features such as easy log-in and sign-up processes, users can quickly create an account and access the app's functionalities. The app allows users to input their medications, including details like name, dosage, and schedule, providing a centralized platform for managing their medication regimen. Customizable reminders ensure that users never miss a dose, with notifications prompting them to take their medications on time. 

# ⚙️ Features
**Log In**
- Allows users to access their account by entering their signed-up credentials. 
- User login requires a valid email address and password of an existing signed-up account. 

**Sign Up**
- Enables users to create an account by providing their basic information. 
- Users need to provide a valid email address and password for account creation. Email address must have an email prefix and existing domain. Password must have eight (8) characters minimum with at least one lowercase character, uppercase character, number (0-9), and symbol. 

**Forgot Password**

- Provides a way for users to reset their password if they happen to forget it. 
- Resetting a user’s password will be done via email verification through a generated code that needs to be input for successful password reset. 

**Medication Input**
- Allows users to add their medications to the app, specifying details like name, dosage, schedule (see Feature B.7), prescribed (see Feature B.10) or non-prescribed. 
- Medication inputs are limited to the medication’s name, dosage amount, dosage form (e.g. fluid, capsule, tablet, etc.), dosage unit total amount, total amount unit (e.g. mL, tablets, etc.), and expiration date. Medications are categorized as either prescribed or non-prescribed. Medication inputs can be edited and deleted by the user accordingly. 

**Refill Medication Reminder Notifications**
- Sends notifications to remind users to refill their medications. 
- Users will only be reminded/notified if a specific medication’s tracked amount is concluded to be empty or almost empty. 

**Take Medication Reminder Notifications** 
- Sends notifications to remind users to take their medications. 
- Users will only be reminded/notified on the specific time and period that was set. 

**Scheduling, Tracking, and Logging** 
- Scheduling is required in medication input. It enables users to schedule and track their medication intake times, and to log each dose taken. 
- In scheduling medication intake, intake frequency is limited to one (1) to four (4) distinct times per period only, and intake periods are limited to “As Needed” (PRN), “Daily”, and “Weekly." Weekly periods require a specific valid date to determine the weekday of intake. Logging each dose taken only needs the user’s input (e.g. clicking a “Taken” button) upon dosage intake. Schedules can be edited by the user. 

**Tracking Dosage Intake via Logs** 
- Allows users to monitor their medication adherence by tracking when doses are taken through their respective logs. 
- A user’s medication intake logs can only show the log date & time, medication’s name, dosage value, and the user’s cumulative intake. Logs cannot be edited by the user but can be cleared. 

**Customizable Reminder Notifications** 
- Provides users with the option to customize medication reminder settings according to their preferences. 
- Reminder setting customization are limited to the reminder’s message title and text only. 

**Prescriptions & Doctor Prescribed**
- Medication inputs can be either non-prescribed or prescribed. Prescribed medications have additional details such as intake start date, end date, and instructions. Each prescribed medication has a doctor who prescribed it. 
- Prescribed medication date range must include valid dates, instructions are optional to the user, and each prescribed medication must have a doctor prescribed. A doctor’s information contains their name, specialization, email, and affiliation. Prescription and doctor information can be edited by the user. 

**User Profile** 
- Users have their own profile that displays and summarizes their basic pertinent information. Users can also edit to update their profile’s information. 
- Users can only view their pertinent information and can only edit their user profile’s information accordingly. 
