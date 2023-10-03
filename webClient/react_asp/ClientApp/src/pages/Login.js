import React, { useState } from 'react';
import { useSelector, useDispatch } from 'react-redux'
import { decrement, increment } from '../reducers/counterSlice'

const LoginPage = ({ login }) => {


  const count = useSelector((state: RootState) => state.counter.value)
  const dispatch = useDispatch();


  const [formData, setFormData] = useState({
    email: '',
    password: '',
  });

  const { email, password } = formData;

  const handleChange = (e) => {
    setFormData({ ...formData, [e.target.name]: e.target.value });
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    console.log(formData);
  };

  return (
    <div>
      <h2>Login</h2>
      <button
          aria-label="Increment value"
          onClick={() => dispatch(increment())}
        >checking to see redux state work</button>
      <form onSubmit={handleSubmit}>
        <div>
          <label>Email:</label>
          <input
            type="email"
            name="email"
            value={email}
            onChange={handleChange}
            required
          />
        </div>
        <div>
          <label>Password:</label>
          <input
            type="password"
            name="password"
            value={password}
            onChange={handleChange}
            required
          />
        </div>
        <div>
          <button type="submit">Login</button>
        </div>
      </form>
    </div>
  );
}

export default LoginPage;
