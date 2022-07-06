package common.api_setup.api_common;

/*
Defined all user details that needs to be sent in one JSON object when executing basic CRUD operations
 */

import com.fasterxml.jackson.annotation.JsonAutoDetect;

import java.io.Serializable;


@JsonAutoDetect(fieldVisibility = JsonAutoDetect.Visibility.ANY)
public class User implements Serializable {

    private String name;

    private String lastname;

    private String email;

    private String password;

    private int department;

    public User(){

    };

    public User(String name, String lastname, String email, String password, int department, int role, String position, String avatarUrl) {
        this.name = name;
        this.lastname = lastname;
        this.email = email;
        this.password = password;
        this.department = department;
        this.role = role;
        this.position = position;
        this.avatarUrl = avatarUrl;
    }

    private int role;

    private String position;

    private String avatarUrl;


    public void setName(String name) {
        this.name = name;
    }

    public void setLastname(String lastname) {
        this.lastname = lastname;
    }

    public void setEmail(String email) {
        this.email = email;
    }

    public void setPassword(String password) {
        this.password = password;
    }

    public void setDepartment(int department) {
        this.department = department;
    }

    public void setRole(int role) {
        this.role = role;
    }

    public void setPosition(String position) {
        this.position = position;
    }

    public void setAvatarUrl(String avatarUrl) {
        this.avatarUrl = avatarUrl;
    }


}
