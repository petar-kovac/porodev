import { FC } from 'react';
import styled from 'styled-components';
import { StorageKey } from 'util/enums/storage-keys';

const Profile: FC = () => {
  return (
    <StyledPage>
      <StyledProfileCard>
        <p style={{ textAlign: 'center' }}>Profile</p>
        <p>Name &rarr; {localStorage.getItem(StorageKey.NAME)}</p>
        <p>Last name &rarr; {localStorage.getItem(StorageKey.LASTNAME)}</p>
      </StyledProfileCard>
    </StyledPage>
  );
};

const StyledPage = styled.div`
  margin-top: 5rem;
  display: flex;
  justify-content: center;
  align-items: center;
`;

const StyledProfileCard = styled.div`
  padding: 3rem 8rem;
  background-color: #fcfcfc;
  border-radius: 30px;
  box-shadow: 0 1px #ffffff inset, 1px 3px 8px rgba(34, 25, 25, 0.2);
  margin-bottom: 5rem;
`;

export default Profile;
