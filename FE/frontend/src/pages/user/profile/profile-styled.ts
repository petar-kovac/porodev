import styled from 'styled-components';

const StyledPage = styled.div`
  margin-top: 5rem; ;
`;

const StyledProfileCard = styled.div`
  max-width: 80rem;
  margin: 0 auto;
  padding: 3rem 8rem;
  background-color: #fcfcfc;
  border-radius: 30px;
  box-shadow: 0 1px #ffffff inset, 1px 3px 8px rgba(34, 25, 25, 0.2);
  display: flex;
  flex-direction: column;
  align-items: center;
  gap: 3rem;
`;

const StyledProfileCardContent = styled.div`
  width: 100%;
  display: flex;
  flex-direction: column;
  gap: 2rem;
`;

const StyledProfileCardItem = styled.div<{ avatar?: boolean }>`
  display: flex;
  justify-content: space-between;
  align-items: ${({ avatar }) => (avatar ? 'flex-end' : 'center')};
  border-bottom: 1px solid #ddd;
  padding: 0 1.5rem;
  border-bottom-left-radius: 0.8rem;
  border-bottom-right-radius: 0.8rem;

  .ant-avatar {
    border: 1px solid #999;
    margin-bottom: 0.5rem;
  }
`;

const StyledProfileIcon = styled.div`
  cursor: pointer;
`;
