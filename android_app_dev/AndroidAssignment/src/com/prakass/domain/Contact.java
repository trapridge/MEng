package com.prakass.domain;

public class Contact {
    private long id;
    private String firstName;
    private String lastName;
    private String phoneNumber;
    private String email;
    
    public Contact(){}
    
    public Contact(String firstName, String lastName, String phoneNumber, String email) {
	super();
	this.firstName = firstName;
	this.lastName = lastName;
	this.phoneNumber = phoneNumber;
	this.email = email;
    }
    public String getFirstName() {
        return firstName;
    }
    public void setFirstName(String firstName) {
        this.firstName = firstName;
    }
    public String getLastName() {
        return lastName;
    }
    public void setLastName(String lastName) {
        this.lastName = lastName;
    }
    public String getPhoneNumber() {
        return phoneNumber;
    }
    public void setPhoneNumber(String phoneNumber) {
        this.phoneNumber = phoneNumber;
    }
    public String getEmail() {
        return email;
    }
    public void setEmail(String email) {
        this.email = email;
    }
    
    public long getId() {
        return id;
    }
    public void setId(long id) {
        this.id = id;
    }
    @Override
    public String toString() {
	String contact = "";
	contact+= "Full name: "+ this.firstName+ " "+ this.lastName;
	contact+= "\nPhone number: "+this.phoneNumber;
	contact+= "\nEmail address: "+this.email;
	return contact;
    }
    
    public String validate(){
	if(this.firstName!=null && this.firstName.length()<1){
	    return "First name can not be blank!";
	}
	if(this.lastName!=null && this.lastName.length()<1){
	    return "Last can not be blank!";
	}
	if(this.phoneNumber!=null && this.phoneNumber.length()<1){
	    return "Phone number can not be blank!";
	}
	return "OK";
    }
}
