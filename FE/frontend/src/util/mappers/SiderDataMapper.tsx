import dayjs from 'dayjs';
import { FC } from 'react';
import styled from 'styled-components';
import { IFilesCard } from 'types/card-data';
import { ApiTranslation } from 'util/enums/api-translation-data';
import { formatDate } from 'util/helpers/date-formaters';

interface MappedCardData {
  [key: string]: string;
}
interface EntryType {
  [key: string]: string | number | boolean;
}

const mapResponseToCardData = (response: IFilesCard): MappedCardData => {
  let obj: MappedCardData = {};
  // eslint-disable-next-line array-callback-return
  Object.entries(response).map(([key, value]: any) => {
    if (key === 'isExe' || key === 'isDeleted' || key === 'isDeleted') {
      console.log('A');
    } else if (key === 'uploadTime') {
      obj = { ...obj, [ApiTranslation.createdAt]: formatDate(value) };
    } else {
      obj = {
        ...obj,
        [ApiTranslation[key]]: value,
      };
    }
  });

  return obj;
};

/**
 * Component to map trough API data.
 */
const SiderData: FC<{ data: IFilesCard }> = ({ data }) => {
  return (
    <>
      {Object.entries(data).map(([key, value]) => {
        if (
          ApiTranslation[key as any] === 'Upload time' ||
          ApiTranslation[key as any] === 'Added'
        ) {
          return (
            <StyledText>
              <Left>{[ApiTranslation[key as any]]}:</Left>
              <Right>{formatDate(value)}</Right>
            </StyledText>
          );
        }

        if (ApiTranslation[key as any] === 'File size') {
          return (
            <StyledText>
              <Left>{[ApiTranslation[key as any]]}:</Left>
              <Right>{value / 1000} KB</Right>
            </StyledText>
          );
        }
        // eslint-disable-next-line consistent-return
        return (
          <div key={key}>
            {ApiTranslation[key as any] && (
              <StyledText>
                <Left>{[ApiTranslation[key as any]]}:</Left>
                <Right>{`${value}`}</Right>
              </StyledText>
            )}
          </div>
        );
      })}
    </>
  );
};

const StyledText = styled.div`
  display: flex;
  max-height: 120px;
  word-break: break-all;
`;
const Left = styled.div`
  flex: 0.5;
  font-weight: 500;
`;
const Right = styled.div`
  flex: 1;
`;

export default SiderData;
