import axios from 'axios';
import { useState, useEffect, FC } from 'react';
import { Link, useParams, useLocation } from 'react-router-dom';
import { verifyEmail } from 'service/files/files';
import styled from 'styled-components';
import emailImage from '../../assets/email-sent.png';
import success from '../../assets/success.png';

const EmailVerify: FC = () => {
  const [loading, setLoading] = useState(false);
  const [validUrl, setValidUrl] = useState(true);
  const { email, token } = useParams();

  // const { search } = useLocation();
  // const searchParams = new URLSearchParams(search);
  // const searchEmail = searchParams.get('Email');
  // const searchToken = searchParams.get('Token');

  // console.log(searchEmail, searchToken);

  // useEffect(() => {
  //   verifyEmail(searchEmail, searchToken)
  //     .then((response: any) => {
  //       console.log(response);
  //       setValidUrl(true);
  //     })
  //     .catch((error: any) => {
  //       console.log(error);
  //       setValidUrl(false);
  //     })
  //     .finally(() => {
  //       setLoading(false);
  //     });
  // }, []);

  if (loading) {
    return (
      <>
        <p>Loading...</p>
      </>
    );
  }

  console.log(email, token);

  return (
    <>
      {validUrl ? (
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
            <img
              src={emailImage}
              alt="success_img"
              style={{ maxWidth: '70rem' }}
            />

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
      ) : (
        <h1>404 Not Found</h1>
      )}
    </>
  );
};

export default EmailVerify;
