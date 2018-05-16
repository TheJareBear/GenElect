## GenElect

Online elective registrator for GenCyber camp at Dakota State University

Written using ASP MVC built in Visual Studio 2015 (though operable in VS 2017)

On build, landing page looks like:

![image](https://user-images.githubusercontent.com/25625288/40135448-9c457144-5902-11e8-856a-ef5592361337.png)

On login page, creds for students will be email and the password given at start of week:

![image](https://user-images.githubusercontent.com/25625288/40135657-14030926-5903-11e8-8b37-bd8a1f22f906.png)

# Students Management

Electives page will look like below, clicking register will succeed if they have an open slot for the period and the elective is not already full:

![image](https://user-images.githubusercontent.com/25625288/40135767-576c3a20-5903-11e8-868f-2e560d157a67.png)

Schedule will look like below (note, at the end of each day students will need elective id's set back to 0 so that errors aren't created when electives are removed):

![image](https://user-images.githubusercontent.com/25625288/40135885-a80408f0-5903-11e8-8152-217ca0063c40.png)

# Admin Management

This is the admin page layout:

![image](https://user-images.githubusercontent.com/25625288/40135912-bf038a9e-5903-11e8-9d77-da3c51298d63.png)

Roles will not need to be managed very heavily, but student user accounts will have to be created before the camp:

![image](https://user-images.githubusercontent.com/25625288/40135978-dfe03a32-5903-11e8-8d01-bd9870b75eee.png)

Creating and editing electives will be the majority of the work done on the site itself:

![image](https://user-images.githubusercontent.com/25625288/40136060-1637ec10-5904-11e8-93fb-3b1e8cde44a3.png)

![image](https://user-images.githubusercontent.com/25625288/40136078-22313f58-5904-11e8-9e85-8fc140784f6a.png)

![image](https://user-images.githubusercontent.com/25625288/40136093-2c6b7218-5904-11e8-9dae-d81efef99ef8.png)

Periods shouldn't require much management either, but I included it just in case


# Final notes:
1. There is no way to change current password of a user or the admin (which is currently admin@genelect.com:Password1!) but a solution would be to hash the password yourself and change the database entry or creating a new admin user and then deleting the current admin from the database.
2. The users selected electives need to be reset after each day when electives are deleted. I hope to update so the admin only has to press one button to reset all current registrations. Otherwise when electives are removed, the schedule portion of the application will try to grab info about an elective that has been removed.
3. Users need to be entered manually through the create user functionallity in the site or through the database AspNetUsers table itself. I also want to update this so that users can be added by uploading an excel or text document that has the required info (future todo)
4. Enjoy the camp! Hope this can help a little this year and as I get some feedback I will be sure to fine tune it for the years to come.
