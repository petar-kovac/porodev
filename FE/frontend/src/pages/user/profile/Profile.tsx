import { FC } from 'react';
import styled from 'styled-components';

import { StorageKey } from 'util/enums/enums';

import { useAuthStateValue } from 'context/AuthContext';

const Profile: FC = () => {
  const { isAuthenticated, testMessage, loggedUser } = useAuthStateValue();

  return (
    <StyledPage>
      <h1>Logged user:</h1>
      <h3>Name - {localStorage.getItem(StorageKey.NAME)}</h3>
      <h3>Last name - {localStorage.getItem(StorageKey.LASTNAME)}</h3>
    </StyledPage>
  );
};

const StyledPage = styled.div`
  padding: 20px;
`;
export default Profile;
