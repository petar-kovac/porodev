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
      <h3>Files data</h3>
      <p>Number of uploaded files: {numberOfUploadedFiles}</p>
      <p>Number of deleted files: {numberOfDeletedFiles}</p>
      <p>Number of users: {numberOfUsers}</p>
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

export default DashboardCard;
