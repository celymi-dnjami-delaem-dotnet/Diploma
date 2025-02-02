import { UserPosition } from '../constants/userConstants';
import { IJsonPatchBody } from '../types';
import { ISelectedItem } from '../types/storyTypes';
import { IUser } from '../types/userTypes';

export const createUserPositionDropdownItems = (): ISelectedItem[] =>
    Object.entries(UserPosition).map(
        ([key, value]) =>
            ({
                key,
                value,
            } as ISelectedItem)
    );

export const createRequestBodyForUserUpdateLink = (userId: string, avatarLink: string): IJsonPatchBody[] => [
    {
        op: 'add',
        path: '/userId',
        value: userId,
    },
    {
        op: 'add',
        path: '/avatarLink',
        value: avatarLink,
    },
];

export const createRequestBodyForUserChangeStatus = (userId: string, isActive: boolean): IJsonPatchBody[] => [
    {
        op: 'add',
        path: '/userId',
        value: userId,
    },
    {
        op: 'add',
        path: '/isActive',
        value: String(isActive),
    },
];

export const createAvailableUsersDropdownItems = (requiredPosition: UserPosition, users: IUser[]): ISelectedItem[] =>
    users.reduce(
        (acc, x) =>
            x.userPosition && UserPosition[x.userPosition] === requiredPosition
                ? [...acc, { key: x.userId, value: x.userName } as ISelectedItem]
                : acc,
        [{ key: '', value: 'No Owner' } as ISelectedItem]
    );
