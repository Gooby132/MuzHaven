import React, { useState } from 'react';
import { v4 as uuidv4 } from 'uuid'; // Import uuid
const axios = require('axios');

const Register = () => {
  const [formData, setFormData] = useState({
    firstName: '',
    lastName: '',
    stageName: '',
    email: '',
    bio: '',
    password: '',
    profile: null, // To hold the selected file
  });

  const [errors, setErrors] = useState({});

  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormData({
      ...formData,
      [name]: value,
    });
  };

  const handleFileChange = (e) => {
    const file = e.target.files[0];
    setFormData({
      ...formData,
      profile: file,
    });
  };

  const handleSubmit = (e) => {
    e.preventDefault();

    // Validate the form data
    const validationErrors = {};
    if (formData.bio.length < 2 || formData.bio.length > 150) {
      validationErrors.bio = 'Bio must be between 2 and 150 characters';
    }
    if (formData.password.length < 8) {
      validationErrors.password = 'Password must be at least 8 characters';
    }

    if (Object.keys(validationErrors).length > 0) {
      setErrors(validationErrors);
    } else {
      // Form data is valid, you can submit it to your server
      // Include formData in your API request
      console.log('Form data:', formData);

      // Reset the form and errors
      setFormData({
        firstName: '',
        lastName: '',
        stageName: '',
        email: '',
        bio: '',
        password: '',
        profile: null,
      });
      setErrors({});

      try {

        fetch('weatherforecast ',
          {
            method: 'POST',
            // body: new FormData(document.querySelector('form'))
          })
          .then(response => console.log(response))
          .catch((error) => {
            console.log('error happend in fetch');
          });

      } catch (err) {
        console.error(err)
      }
    }
  };

  return (
    <div>
      <h1>Register Me</h1>
      <form onSubmit={handleSubmit}>
        <div>
          <label>First Name:</label>
          <input
            type="text"
            name="firstName"
            value={formData.firstName}
            onChange={handleChange}
          />
        </div>
        <div>
          <label>Last Name:</label>
          <input
            type="text"
            name="lastName"
            value={formData.lastName}
            onChange={handleChange}
          />
        </div>
        <div>
          <label>Stage Name:</label>
          <input
            type="text"
            name="stageName"
            value={formData.stageName}
            onChange={handleChange}
          />
        </div>
        <div>
          <label>Email:</label>
          <input
            type="text"
            name="email"
            value={formData.email}
            onChange={handleChange}
          />
        </div>
        <div>
          <label>Bio:</label>
          <textarea
            name="bio"
            value={formData.bio}
            onChange={handleChange}
          />
          {errors.bio && <span className="error">{errors.bio}</span>}
        </div>
        <div>
          <label>Password:</label>
          <input
            type="password"
            name="password"
            value={formData.password}
            onChange={handleChange}
          />
          {errors.password && <span className="error">{errors.password}</span>}
        </div>
        <div>
          <label>Profile Picture:</label>
          <input
            type="file"
            name="profile"
            accept="image/*"
            onChange={handleFileChange}
          />
        </div>
        <div>
          <button type="submit">Register</button>
        </div>
      </form>
    </div>
  );
};

export default Register;
