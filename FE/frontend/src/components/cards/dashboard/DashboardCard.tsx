import { FC } from 'react';
import styled from 'styled-components';

interface IDashboardCardProps {
  title?: string;
  numberOfUploadedFiles?: any;
  numberOfDeletedFiles?: any;
  numberOfUsers?: any;
}

const DashboardCard: FC<IDashboardCardProps> = ({
  title,
  numberOfUploadedFiles,
  numberOfDeletedFiles,
  numberOfUsers,
}) => {
  // const capitalizedTitle = title.charAt(0).toUpperCase() + title.slice(1);

  return (
    <StyledDashboardCard>
      {/* <h2>Files data</h2> */}
      <h2>{title}</h2>
      <p>
        Number of uploaded files:{' '}
        <StyledSpan>{numberOfUploadedFiles}</StyledSpan>
      </p>
      <p>
        Number of deleted files: <StyledSpan>{numberOfDeletedFiles}</StyledSpan>
      </p>
      <p>
        Number of users: <StyledSpan>{numberOfUsers}</StyledSpan>
      </p>
    </StyledDashboardCard>
  );
};
const StyledDashboardCard = styled.div`
  background-color: #fff;
  border: 1px solid #ddd;
  border-radius: 2rem;
  padding: 2rem;
  width: 25rem;
`;

const StyledSpan = styled.span`
  font-size: 1.5rem;
  font-weight: bold;
`;

export default DashboardCard;
