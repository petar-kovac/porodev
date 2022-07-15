import axios from 'axios';
import { useState, useEffect, FC } from 'react';
import { Link, useParams, useLocation } from 'react-router-dom';
import { useAuthStateValue } from 'context/AuthContext';
import { verifyEmail } from 'service/files/files';
import styled from 'styled-components';
import emailImage from '../../assets/email-sent.png';
import success from '../../assets/success.png';

const EmailVerify: FC = () => {
  return (
    <div
      style={{
        width: '100vw',
        height: '100vh',
        display: 'flex',
        alignItems: 'center',
        justifyContent: 'center',
        flexDirection: 'column',
        gap: '3rem',
        backgroundColor: '#f3f3f3',
      }}
    >
      <div
        style={{
          backgroundColor: '#fff',
          display: 'flex',
          alignItems: 'center',
          padding: '4rem',
          justifyContent: 'center',
          flexDirection: 'column',
        }}
      >
        <img src={success} alt="tick" style={{ width: '12rem' }} />
        <h1>Registration successful!</h1>
      </div>
      <div
        style={{
          backgroundColor: '#fff',
          display: 'flex',
          flexDirection: 'column',
          alignItems: 'center',
          paddingBottom: '3rem',
          justifyContent: 'center',
        }}
      >
        <img src={emailImage} alt="success_img" style={{ maxWidth: '70rem' }} />

        <h1>Verify your email</h1>
        <p>
          We&apos;ve sent an email to you email address. Check your inbox in
          order to verify.
        </p>
        {/* <Link to="/login">
              <button type="button">Proceed to login</button>
            </Link> */}
      </div>
    </div>
  );
};

export default EmailVerify;
