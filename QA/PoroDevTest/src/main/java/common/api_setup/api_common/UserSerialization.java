package common.api_setup.api_common;


public class UserSerialization {

    private String name;

    private String lastname;

    private String email;

    private String password;

    private int department;

    private int role;

    private String position;

    private String avatarUrl;


    public UserSerialization() {

    }
    private UserSerialization(String name, String lastname, String email, String password, int department,
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

    private String getName() {
        return name;
    }
    private void setName(String name) {
        this.name = name;
    }
    private String getLastname() {
        return lastname;
    }
    private void setLastname(String lastname) {
        this.lastname = lastname;
    }

    private String getEmail() {
        return email;
    }
    private void setEmail(String email) {
        this.email = email;
    }
    private String getPassword() {
        return  password;
    }
    private void setPassword(String password) {
        this.password = password;
    }
    private int getDepartment() {
        return department;
    }
    private void setDepartment(int department) {
        this.department = department;
    }
    private int getRole() {
        return role;
    }
    private void setRole(int role){
        this.role = role;
    }
    private String getPosition() {
        return position;
    }
    private void setPosition(String position){
        this.position = position;
    }
    private String getAvatarUrl() {
        return avatarUrl;
    }
    private void setAvatarUrl(String avatarUrl){
        this.avatarUrl = avatarUrl;
    }


}
