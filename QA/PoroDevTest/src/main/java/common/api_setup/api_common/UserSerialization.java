package common.api_setup.api_common;


public class UserSerialization {

     String name;

     String lastname;

     String email;

     String password;

     int department;

     int role;

     String position;

     String avatarUrl;


    public UserSerialization() {

    }
    public UserSerialization(String name, String lastname, String email, String password, int department,
                             int role, String position, String avatarUrl) {
        User user = new User();
        user.setName(name);
        user.setLastname(lastname);
        user.setEmail(email);
        user.setPassword(password);
        user.setDepartment(department);
        user.setRole(role);
        user.setPosition(position);
        user.setAvatarUrl(avatarUrl);
}

    public String getName() {
        return name;
    }
    public void setName(String name) {
        this.name = name;
    }
    public String getLastname() {
        return lastname;
    }
    public void setLastname(String lastname) {
        this.lastname = lastname;
    }

    public String getEmail() {
        return email;
    }
    public void setEmail(String email) {
        this.email = email;
    }
    public String getPassword() {
        return  password;
    }
    public void setPassword(String password) {
        this.password = password;
    }
    public int getDepartment() {
        return department;
    }
    public void setDepartment(int department) {
        this.department = department;
    }
    public int getRole() {
        return role;
    }
    public void setRole(int role){
        this.role = role;
    }
    public String getPosition() {
        return position;
    }
    public void setPosition(String position){
        this.position = position;
    }
    public String getAvatarUrl() {
        return avatarUrl;
    }
    public void setAvatarUrl(String avatarUrl){
        this.avatarUrl = avatarUrl;
    }


}
