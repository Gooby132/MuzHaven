import React, { useState } from 'react';
import { useSelector, useDispatch } from 'react-redux'
import { addToken } from '../reducers/tokenSlice'

export const Login = ({ login }) => {


  const count = useSelector((state: RootState) => state.tokenState.value)
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

    try {

      fetch('https://localhost:7026/weatherforecast ',
        {
          method: 'POST',
        })
        .then(response => {
          response.json()
            .then(res => {
              dispatch(addToken(res.token));
            })

        })

        .catch((error) => {
          console.log('error happend in fetch');
        });

    } catch (err) {
      console.error(err)
    }
  };


  return (
    <div className="w-full max-w-xs  m-auto justify-center">
      <form onSubmit={handleSubmit}>
        <div className='mb-6'>
          <label class='bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500' >Email:</label>
          <input
            type="email"
            className='bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500'
            name="email"
            value={email}
            onChange={handleChange}
            required
          />
        </div>
        <div>
          <label className='bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500'>Password:</label>
          <input
            type="password"
            className='bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500'
            name="password"
            value={password}
            onChange={handleChange}
            required
          />
        </div>
        <div>
          <button type="submit"
            className='mt-5 w-full text-white bg-blue-700 hover:bg-blue-800 focus:ring-4 focus:outline-none focus:ring-blue-300 font-medium rounded-lg text-sm w-full px-5 py-2.5 text-center dark:bg-blue-600 dark:hover:bg-blue-700 dark:focus:ring-blue-800'
          >Login</button>
        </div>
      </form>
    </div>
  );
}